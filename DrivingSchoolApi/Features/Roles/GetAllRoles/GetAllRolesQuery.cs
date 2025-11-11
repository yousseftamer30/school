
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Roles.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<List<TbRole>>;

    public class Handler : IRequestHandler<GetAllRolesQuery, List<TbRole>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbRole>> Handle(GetAllRolesQuery request, CancellationToken ct)
        {
            return await _db.TbRoles.AsNoTracking().ToListAsync(ct);
        }
    }
}
