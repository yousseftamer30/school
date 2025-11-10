
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<List<Role>>;

    public class Handler : IRequestHandler<GetAllRolesQuery, List<Role>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken ct)
        {
            return await _db.Roles.AsNoTracking().ToListAsync(ct);
        }
    }
}
