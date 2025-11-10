
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.GetOneRole
{
    public record GetOneRoleQuery(int Id) : IRequest<Role?>;

    public class Handler : IRequestHandler<GetOneRoleQuery, Role?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Role?> Handle(GetOneRoleQuery request, CancellationToken ct)
        {
            return await _db.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.RoleId == request.Id, ct);
        }
    }
}
