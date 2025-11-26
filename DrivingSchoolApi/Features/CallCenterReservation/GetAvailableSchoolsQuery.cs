using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CallCenterReservation
{
    // QUERY
    public record GetAvailableSchoolsQuery : IRequest<List<AvailableSchoolDto>>
    {
        public int LicenseId { get; init; }
        public int? GovId { get; init; } // اختياري للتصفية حسب المحافظة
    }

    // HANDLER
    public class GetAvailableSchoolsHandler : IRequestHandler<GetAvailableSchoolsQuery, List<AvailableSchoolDto>>
    {
        private readonly DrivingSchoolDbContext _context;

        public GetAvailableSchoolsHandler(DrivingSchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<AvailableSchoolDto>> Handle(GetAvailableSchoolsQuery request, CancellationToken ct)
        {
            var query = _context.TbSchools
                .Include(s => s.Gov)
                .Include(s => s.SchoolLicenses)
                .Include(s => s.Reservations)
                .Where(s => s.SchoolLicenses.Any(sl =>
                    sl.LicenseId == request.LicenseId &&
                    sl.IsAvailable
                ));

            // تصفية حسب المحافظة إذا تم تحديدها
            if (request.GovId.HasValue)
            {
                query = query.Where(s => s.GovId == request.GovId.Value);
            }

            var schools = await query
                .Select(s => new AvailableSchoolDto
                {
                    SchoolId = s.SchoolId,
                    SchoolName = s.SchoolName,
                    GovName = s.Gov.GovName ?? "",
                    Location = s.Location ?? "",
                    TotalCapacity = s.TotalCapacity,
                    CurrentEnrollments = s.Reservations.Count(r =>
                        r.LicenseId == request.LicenseId &&
                        r.Status == ReservationStatus.Active
                    ),
                    IsAvailable = s.SchoolLicenses.Any(sl =>
                        sl.LicenseId == request.LicenseId &&
                        sl.IsAvailable
                    )
                })
                ///***////
                //.Where(s => s.CurrentEnrollments < s.TotalCapacity) // فقط المدارس اللي فيها مكان
                .OrderBy(s => s.GovName)
                .ThenBy(s => s.SchoolName)
                .ToListAsync(ct);

            return schools;
        }
    }
}
