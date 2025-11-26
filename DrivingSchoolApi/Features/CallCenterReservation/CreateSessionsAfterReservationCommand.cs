using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.SessionAttendances.CreateSessionsAfterReservation;

// ============== COMMAND ==============
public record CreateSessionsAfterReservationCommand : IRequest<CreateSessionsResponseDto>
{
    public int ReservationId { get; init; }
    public List<SessionScheduleDto> Sessions { get; init; }
}

public record SessionScheduleDto
{
    public int CourseId { get; init; }
    public int InstructorId { get; init; }
    public DateTime SessionDate { get; init; }
    public TimeSpan StartTime { get; init; }
    public int DurationHours { get; init; } // بالساعات الكاملة فقط
    public string Notes { get; init; }
}

// ============== RESPONSE DTO ==============
public record CreateSessionsResponseDto
{
    public int ReservationId { get; init; }
    public string CustomerName { get; init; }
    public string LicenseName { get; init; }
    public string SchoolName { get; init; }
    public int TotalSessionsCreated { get; init; }
    public List<SessionAttendanceDto> CreatedSessions { get; init; }
}

public record SessionAttendanceDto
{
    public int AttendanceId { get; init; }
    public int CourseId { get; init; }
    public string CourseName { get; init; }
    public SessionType SessionType { get; init; }
    public int InstructorId { get; init; }
    public string InstructorName { get; init; }
    public DateTime SessionDate { get; init; }
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public int DurationHours { get; init; }
    public AttendanceStatus AttendanceStatus { get; init; }
}

// ============== VALIDATOR ==============
public class Validator : AbstractValidator<CreateSessionsAfterReservationCommand>
{
    public Validator()
    {
        RuleFor(x => x.ReservationId)
            .GreaterThan(0)
            .WithMessage("ReservationId must be greater than 0");

        RuleFor(x => x.Sessions)
            .NotEmpty()
            .WithMessage("At least one session must be provided");

        RuleForEach(x => x.Sessions).ChildRules(session =>
        {
            session.RuleFor(s => s.CourseId)
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than 0");

            session.RuleFor(s => s.InstructorId)
                .GreaterThan(0)
                .WithMessage("InstructorId must be greater than 0");

            session.RuleFor(s => s.SessionDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("SessionDate must be today or in the future");

            session.RuleFor(s => s.DurationHours)
                .InclusiveBetween(1, 8)
                .WithMessage("DurationHours must be between 1 and 8 hours");

            session.RuleFor(s => s.StartTime)
                .Must(time => time.Minutes == 0 && time.Seconds == 0)
                .WithMessage("StartTime must be on the hour (no minutes or seconds)");
        });
    }
}

// ============== HANDLER ==============
public class Handler : IRequestHandler<CreateSessionsAfterReservationCommand, CreateSessionsResponseDto>
{
    private readonly DrivingSchoolDbContext _context;

    public Handler(DrivingSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<CreateSessionsResponseDto> Handle(
        CreateSessionsAfterReservationCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate Reservation exists and is Active
        var reservation = await _context.TbReservations
            .Include(r => r.Customer)
            .Include(r => r.LicenseType)
            .Include(r => r.School)
                .ThenInclude(s => s.OperatingHours)
            .FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId, cancellationToken);

        if (reservation == null)
            throw new Exception($"Reservation {request.ReservationId} not found");

        if (reservation.Status != ReservationStatus.Active)
            throw new Exception($"Reservation must be Active. Current status: {reservation.Status}");

        var createdSessions = new List<TbSessionAttendance>();

        foreach (var sessionDto in request.Sessions)
        {
            // 2. Validate Course exists and belongs to the license
            var course = await _context.TbCourseLists
                .FirstOrDefaultAsync(c => c.CourseId == sessionDto.CourseId, cancellationToken);

            if (course == null)
                throw new Exception($"Course {sessionDto.CourseId} not found");

            if (course.LicenseId != reservation.LicenseId)
                throw new Exception($"Course '{course.CourseName}' does not belong to license '{reservation.LicenseType.LicenseName}'");

            // 3. Validate Instructor exists, is active, and works at the same school
            var instructor = await _context.TbEmployees
                .Include(e => e.LicenseExpertises)
                    .ThenInclude(le => le.LicenseGroup)
                        .ThenInclude(lg => lg.LicenseGroupMembers)
                .FirstOrDefaultAsync(e => e.EmployeeId == sessionDto.InstructorId, cancellationToken);

            if (instructor == null)
                throw new Exception($"Instructor {sessionDto.InstructorId} not found");

            if (!instructor.IsActive)
                throw new Exception($"Instructor '{instructor.EmployeeName}' is not active");

            if (instructor.SchoolId != reservation.SchoolId)
                throw new Exception($"Instructor '{instructor.EmployeeName}' does not work at school '{reservation.School.SchoolName}'");

            // 4. Validate Instructor can teach this license type
            var canTeachLicense = instructor.LicenseExpertises.Any(le =>
                le.LicenseGroup.LicenseGroupMembers.Any(lgm => lgm.LicenseId == reservation.LicenseId) &&
                ((course.SessionType == SessionType.Theory && le.CanTeachTheory) ||
                 (course.SessionType == SessionType.Practical && le.CanTeachPractical))
            );

            if (!canTeachLicense)
            {
                var sessionTypeAr = course.SessionType == SessionType.Theory ? "نظري" : "عملي";
                throw new Exception($"Instructor '{instructor.EmployeeName}' cannot teach {sessionTypeAr} for license '{reservation.LicenseType.LicenseName}'");
            }

            // 5. ✅ التحقق من أن الوقت على الساعة الكاملة (تم بالفعل في Validator)
            if (sessionDto.StartTime.Minutes != 0 || sessionDto.StartTime.Seconds != 0)
                throw new Exception($"Start time must be on the hour (e.g., 09:00, 10:00). Invalid: {sessionDto.StartTime}");

            var endTime = sessionDto.StartTime.Add(TimeSpan.FromHours(sessionDto.DurationHours));

            // 6. ✅ التحقق من أن الجلسة ضمن ساعات عمل المدرسة
            var dayOfWeek = (int)sessionDto.SessionDate.DayOfWeek;
            var schoolOperatingHour = reservation.School.OperatingHours
                .FirstOrDefault(oh => oh.DayOfWeek == dayOfWeek && oh.IsWorkingDay);

            if (schoolOperatingHour == null)
            {
                var dayName = sessionDto.SessionDate.ToString("dddd");
                throw new Exception($"School '{reservation.School.SchoolName}' is not open on {dayName}");
            }

            if (sessionDto.StartTime < schoolOperatingHour.StartTime)
                throw new Exception($"Session start time {sessionDto.StartTime} is before school opens at {schoolOperatingHour.StartTime}");

            if (endTime > schoolOperatingHour.EndTime)
                throw new Exception($"Session end time {endTime} is after school closes at {schoolOperatingHour.EndTime}");

            // 7. ✅ إزالة التحقق من تعارض مواعيد المدرب
            // لأن المدرب يمكنه تدريس أكثر من عميل في نفس الوقت

            // 8. Create Session Attendance
            var attendance = new TbSessionAttendance
            {
                ReservationId = request.ReservationId,
                CourseId = sessionDto.CourseId,
                InstructorId = sessionDto.InstructorId,
                SessionDate = sessionDto.SessionDate,
                StartTime = sessionDto.StartTime,
                DurationTime = TimeSpan.FromHours(sessionDto.DurationHours),
                EndTime = endTime,
                AttendanceStatus = AttendanceStatus.Scheduled,
                Notes = sessionDto.Notes
            };

            _context.TbSessionAttendances.Add(attendance);
            createdSessions.Add(attendance);
        }

        await _context.SaveChangesAsync(cancellationToken);

        // 9. Load instructor and course names for response
        foreach (var session in createdSessions)
        {
            await _context.Entry(session)
                .Reference(s => s.Instructor)
                .LoadAsync(cancellationToken);
            await _context.Entry(session)
                .Reference(s => s.Course)
                .LoadAsync(cancellationToken);
        }

        return new CreateSessionsResponseDto
        {
            ReservationId = reservation.ReservationId,
            CustomerName = reservation.Customer.FullName,
            LicenseName = reservation.LicenseType.LicenseName,
            SchoolName = reservation.School.SchoolName,
            TotalSessionsCreated = createdSessions.Count,
            CreatedSessions = createdSessions.Select(s => new SessionAttendanceDto
            {
                AttendanceId = s.AttendanceId,
                CourseId = s.CourseId,
                CourseName = s.Course.CourseName,
                SessionType = s.Course.SessionType,
                InstructorId = s.InstructorId,
                InstructorName = s.Instructor.EmployeeName,
                SessionDate = s.SessionDate,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                DurationHours = (int)s.DurationTime.TotalHours,
                AttendanceStatus = s.AttendanceStatus
            }).ToList()
        };
    }
}

