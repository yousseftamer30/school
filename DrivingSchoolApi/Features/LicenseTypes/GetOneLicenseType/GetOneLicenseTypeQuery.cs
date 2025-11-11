using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.LicenseTypes.GetOneLicenseType
{
    public record GetOneLicenseTypeQuery(int Id) : IRequest<TbLicenseType?>;

    public class Handler : IRequestHandler<GetOneLicenseTypeQuery, TbLicenseType?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbLicenseType?> Handle(GetOneLicenseTypeQuery request, CancellationToken ct)
        {
            return await _db.TbLicenseTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.LicenseId == request.Id, ct);
        }
    }
}
