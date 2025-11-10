using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CourseSessions.CreateCourseSession
{
    public record CreateCourseSessionCommand(
        int SchoolId,
        int LicenseId,
        SessionType SessionType,
        decimal DurationHours,
        int InstructorId
    ) : IRequest<CourseSession>;

    public class Handler : IRequestHandler<CreateCourseSessionCommand, CourseSession>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<CourseSession> Handle(CreateCourseSessionCommand request, CancellationToken ct)
        {
            var entity = new CourseSession
            {
                SchoolId = request.SchoolId,
                LicenseId = request.LicenseId,
                SessionType = request.SessionType,
                DurationHours = request.DurationHours,
                InstructorId = request.InstructorId
            };

            _db.CourseSessions.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
