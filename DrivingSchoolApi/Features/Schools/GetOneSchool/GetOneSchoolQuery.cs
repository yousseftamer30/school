
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Schools.GetOneSchool
{
    public record GetOneSchoolQuery(int Id) : IRequest<School?>;

    public class Handler : IRequestHandler<GetOneSchoolQuery, School?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<School?> Handle(GetOneSchoolQuery request, CancellationToken ct)
        {
            return await _db.Schools
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.SchoolId == request.Id, ct);
        }
    }
}
