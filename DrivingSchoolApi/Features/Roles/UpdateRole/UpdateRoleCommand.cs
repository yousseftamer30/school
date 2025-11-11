
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.UpdateRole
{
    public record UpdateRoleCommand(int Id, string RoleName) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken ct)
        {
            var entity = await _db.TbRoles.FirstOrDefaultAsync(x => x.RoleId == request.Id, ct);
            if (entity == null) return false;

            entity.RoleName = request.RoleName;
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
