
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.LicenseTypes.GetAllLicenseTypes
{
    public record GetAllLicenseTypesQuery() : IRequest<List<LicenseTypeDto>>;

    public class LicenseTypeDto
    {
        public int LicenseId { get; set; }
        public string LicenseName { get; set; }
        public string? Description { get; set; }
    }

    public class Handler : IRequestHandler<GetAllLicenseTypesQuery, List<LicenseTypeDto>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<LicenseTypeDto>> Handle(GetAllLicenseTypesQuery request, CancellationToken ct)
        {
            return await _db.LicenseTypes.AsNoTracking()
                .Select(x => new LicenseTypeDto
                {
                    LicenseId = x.LicenseId,
                    LicenseName = x.LicenseName,
                    Description = x.Description
                })
                .ToListAsync(ct);
        }
    }
}
