
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Schools.GetAllSchools
{
    public record GetAllSchoolsQuery() : IRequest<List<School>>;

    public class Handler : IRequestHandler<GetAllSchoolsQuery, List<School>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<School>> Handle(GetAllSchoolsQuery request, CancellationToken ct)
        {
            return await _db.Schools.AsNoTracking().ToListAsync(ct);
        }
    }
}
