
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.Roles.CreateRole
{
    public record CreateRoleCommand(string RoleName) : IRequest<TbRole>;

    public class Handler : IRequestHandler<CreateRoleCommand, TbRole>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbRole> Handle(CreateRoleCommand request, CancellationToken ct)
        {
            var entity = new TbRole { RoleName = request.RoleName };
            _db.TbRoles.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
