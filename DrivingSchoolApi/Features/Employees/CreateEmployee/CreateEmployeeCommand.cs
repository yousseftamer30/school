using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;

using MediatR;

namespace DrivingSchoolApi.Features.Employees.CreateEmployee
{
    public record CreateEmployeeCommand(
        string EmployeeName,
        int SchoolId,
        int RoleId,
        bool IsActive
    ) : IRequest<Employee>;

    public class Handler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            var entity = new Employee
            {
                EmployeeName = request.EmployeeName,
                SchoolId = request.SchoolId,
                RoleId = request.RoleId,
                IsActive = request.IsActive
            };

            _db.Employees.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
