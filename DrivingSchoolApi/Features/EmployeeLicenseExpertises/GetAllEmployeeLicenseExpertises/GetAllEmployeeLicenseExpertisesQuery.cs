using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.GetAllEmployeeLicenseExpertises
{
    public record GetAllEmployeeLicenseExpertisesQuery() : IRequest<List<EmployeeLicenseExpertise>>;

    public class Handler : IRequestHandler<GetAllEmployeeLicenseExpertisesQuery, List<EmployeeLicenseExpertise>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<EmployeeLicenseExpertise>> Handle(GetAllEmployeeLicenseExpertisesQuery request, CancellationToken ct)
        {
            return await _db.EmployeeLicenseExpertises
                .Include(e => e.Employee)
                .Include(e => e.LicenseType)
                .ToListAsync(ct);
        }
    }
}
