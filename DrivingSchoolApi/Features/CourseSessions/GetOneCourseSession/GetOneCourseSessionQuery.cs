using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseSessions.GetOneCourseSession
{
    public record GetOneCourseSessionQuery(int SessionId) : IRequest<CourseSession>;

    public class Handler : IRequestHandler<GetOneCourseSessionQuery, CourseSession>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<CourseSession> Handle(GetOneCourseSessionQuery request, CancellationToken ct)
        {
            return await _db.CourseSessions
                .Include(cs => cs.School)
                .Include(cs => cs.LicenseType)
                .Include(cs => cs.Instructor)
                .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, ct);
        }
    }
}
