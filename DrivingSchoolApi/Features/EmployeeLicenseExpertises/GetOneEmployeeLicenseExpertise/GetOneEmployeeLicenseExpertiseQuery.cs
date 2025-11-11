using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.GetOneEmployeeLicenseExpertise
{
    public record GetOneEmployeeLicenseExpertiseQuery(int Id) : IRequest<TbEmployeeLicenseExpertise>;

    public class Handler : IRequestHandler<GetOneEmployeeLicenseExpertiseQuery, TbEmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployeeLicenseExpertise> Handle(GetOneEmployeeLicenseExpertiseQuery request, CancellationToken ct)
        {
            return await _db.TbEmployeeLicenseExpertises
                .Include(e => e.Employee)
                .Include(e => e.LicenseType)
                .FirstOrDefaultAsync(x => x.ExpertiseId == request.Id, ct);
        }
    }
}
