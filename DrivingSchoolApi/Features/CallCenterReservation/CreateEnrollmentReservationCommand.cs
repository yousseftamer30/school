using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CallCenterReservation
{
    public record CreateEnrollmentReservationCommand : IRequest<ReservationCreatedDto>
    {
        public int PaymentId { get; init; }
        public int SchoolId { get; init; }
        public string? Notes { get; init; }
    }

    // HANDLER
    public class CreateEnrollmentReservationHandler : IRequestHandler<CreateEnrollmentReservationCommand, ReservationCreatedDto>
    {
        private readonly DrivingSchoolDbContext _context;

        public CreateEnrollmentReservationHandler(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationCreatedDto> Handle(CreateEnrollmentReservationCommand request, CancellationToken ct)
        {
            // 1.Get Datepayment
            var payment = await _context.TbPayments
                .Include(p => p.Customer)
                .Include(p => p.LicenseType)
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId, ct);

            if (payment == null)
                throw new Exception("عملية الدفع غير موجودة");

            
            if (payment.ReceiptStatus != ReceiptStatus.Valid)
                throw new Exception($"الإيصال غير صالح - الحالة: {payment.ReceiptStatus}");

           
            if (payment.Reservation != null)
                throw new Exception("تم إنشاء حجز لهذا الإيصال من قبل");

           
            var school = await _context.TbSchools
                .Include(s => s.SchoolLicenses)
                .FirstOrDefaultAsync(s => s.SchoolId == request.SchoolId, ct);

            if (school == null)
                throw new Exception("المدرسة غير موجودة");

           
            var schoolLicense = school.SchoolLicenses
                .FirstOrDefault(sl => sl.LicenseId == payment.LicenseId && sl.IsAvailable);

            if (schoolLicense == null)
                throw new Exception("هذه الرخصة غير متاحة في المدرسة المختارة");

            // Create Reservation
            var reservation = new TbReservation
            {
                CustomerId = payment.CustomerId,
                LicenseId = payment.LicenseId,
                SchoolId = request.SchoolId,
                PaymentId = request.PaymentId,
                ReservationDate = DateTime.Now,
                Status = ReservationStatus.Active,
                Notes = request.Notes
            };

            _context.TbReservations.Add(reservation);
            await _context.SaveChangesAsync(ct);

            // Get  Required Courses
            var requiredCourses = await _context.TbCourseLists
                .Where(c => c.LicenseId == payment.LicenseId)
                .Select(c => new CourseRequirementDto
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    SessionType = c.SessionType.ToString(),
                    DurationHours = c.DurationHours
                })
                .ToListAsync(ct);

            //  return DTO
            return new ReservationCreatedDto
            {
                ReservationId = reservation.ReservationId,
                CustomerId = payment.Customer.CustomerId,
                CustomerName = payment.Customer.FullName,
                LicenseName = payment.LicenseType.LicenseName,
                SchoolName = school.SchoolName,
                ReservationDate = reservation.ReservationDate,
                Status = reservation.Status.ToString(),
                RequiredCourses = requiredCourses
            };
        }
    }
}
