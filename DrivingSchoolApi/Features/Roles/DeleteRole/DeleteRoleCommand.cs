
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.DeleteRole
{
    public record DeleteRoleCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken ct)
        {
            var entity = await _db.Roles.FirstOrDefaultAsync(x => x.RoleId == request.Id, ct);
            if (entity == null) return false;

            _db.Roles.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
