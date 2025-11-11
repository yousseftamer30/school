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
    ) : IRequest<TbEmployee>;

    public class Handler : IRequestHandler<CreateEmployeeCommand, TbEmployee>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployee> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            var entity = new TbEmployee
            {
                EmployeeName = request.EmployeeName,
                SchoolId = request.SchoolId,
                RoleId = request.RoleId,
                IsActive = request.IsActive
            };

            _db.TbEmployees.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
