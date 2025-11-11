using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.DeleteEmployee
{
    public record DeleteEmployeeCommand(int EmployeeId) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbEmployees.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, ct);
            if (entity == null) return false;

            _db.TbEmployees.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
