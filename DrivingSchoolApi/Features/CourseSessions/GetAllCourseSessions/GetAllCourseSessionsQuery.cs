using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseSessions.GetAllCourseSessions
{
    public record GetAllCourseSessionsQuery() : IRequest<List<CourseSession>>;

    public class Handler : IRequestHandler<GetAllCourseSessionsQuery, List<CourseSession>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<CourseSession>> Handle(GetAllCourseSessionsQuery request, CancellationToken ct)
        {
            return await _db.CourseSessions
                .Include(cs => cs.School)
                .Include(cs => cs.LicenseType)
                .Include(cs => cs.Instructor)
                .ToListAsync(ct);
        }
    }
}
