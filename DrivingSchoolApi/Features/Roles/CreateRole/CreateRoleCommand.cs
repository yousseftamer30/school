
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.Roles.CreateRole
{
    public record CreateRoleCommand(string RoleName) : IRequest<Role>;

    public class Handler : IRequestHandler<CreateRoleCommand, Role>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Role> Handle(CreateRoleCommand request, CancellationToken ct)
        {
            var entity = new Role { RoleName = request.RoleName };
            _db.Roles.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
