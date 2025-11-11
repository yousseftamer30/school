
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.GetOneRole
{
    public record GetOneRoleQuery(int Id) : IRequest<TbRole?>;

    public class Handler : IRequestHandler<GetOneRoleQuery, TbRole?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbRole?> Handle(GetOneRoleQuery request, CancellationToken ct)
        {
            return await _db.TbRoles.AsNoTracking().FirstOrDefaultAsync(x => x.RoleId == request.Id, ct);
        }
    }
}
