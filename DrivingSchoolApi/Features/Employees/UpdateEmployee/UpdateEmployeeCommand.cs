using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.UpdateEmployee
{
    public record UpdateEmployeeCommand(
        int EmployeeId,
        string EmployeeName,
        int SchoolId,
        int RoleId,
        bool IsActive
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            var entity = await _db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, ct);
            if (entity == null) return false;

            entity.EmployeeName = request.EmployeeName;
            entity.SchoolId = request.SchoolId;
            entity.RoleId = request.RoleId;
            entity.IsActive = request.IsActive;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
