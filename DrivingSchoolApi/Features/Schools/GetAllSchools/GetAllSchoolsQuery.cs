
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Schools.GetAllSchools
{
    public record GetAllSchoolsQuery() : IRequest<List<TbSchool>>;

    public class Handler : IRequestHandler<GetAllSchoolsQuery, List<TbSchool>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbSchool>> Handle(GetAllSchoolsQuery request, CancellationToken ct)
        {
            return await _db.TbSchools.AsNoTracking().ToListAsync(ct);
        }
    }
}
