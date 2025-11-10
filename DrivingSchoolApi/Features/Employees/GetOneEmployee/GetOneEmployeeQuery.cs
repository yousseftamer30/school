using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.GetOneEmployee
{
    public record GetOneEmployeeQuery(int EmployeeId) : IRequest<Employee>;

    public class Handler : IRequestHandler<GetOneEmployeeQuery, Employee>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Employee> Handle(GetOneEmployeeQuery request, CancellationToken ct)
        {
            return await _db.Employees
                .Include(e => e.School)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, ct);
        }
    }
}
