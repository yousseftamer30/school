using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.GetAllEmployeeLicenseExpertises
{
    public record GetAllEmployeeLicenseExpertisesQuery() : IRequest<List<TbEmployeeLicenseExpertise>>;

    public class Handler : IRequestHandler<GetAllEmployeeLicenseExpertisesQuery, List<TbEmployeeLicenseExpertise>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbEmployeeLicenseExpertise>> Handle(GetAllEmployeeLicenseExpertisesQuery request, CancellationToken ct)
        {
            return await _db.TbEmployeeLicenseExpertises
                .Include(e => e.Employee)
                .Include(e => e.LicenseType)
                .ToListAsync(ct);
        }
    }
}
