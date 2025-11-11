using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.GetAllEmployees
{
    public record GetAllEmployeesQuery() : IRequest<List<TbEmployee>>;

    public class Handler : IRequestHandler<GetAllEmployeesQuery, List<TbEmployee>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbEmployee>> Handle(GetAllEmployeesQuery request, CancellationToken ct)
        {
            return await _db.TbEmployees
                .Include(e => e.School)
                .Include(e => e.Role)
                .ToListAsync(ct);
        }
    }
}
