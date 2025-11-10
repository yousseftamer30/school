using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.GetAllEmployees
{
    public record GetAllEmployeesQuery() : IRequest<List<Employee>>;

    public class Handler : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken ct)
        {
            return await _db.Employees
                .Include(e => e.School)
                .Include(e => e.Role)
                .ToListAsync(ct);
        }
    }
}
