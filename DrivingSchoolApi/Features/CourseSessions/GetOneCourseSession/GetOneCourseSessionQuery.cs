using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseSessions.GetOneCourseSession
{
    public record GetOneCourseSessionQuery(int SessionId) : IRequest<TbCourseSession>;

    public class Handler : IRequestHandler<GetOneCourseSessionQuery, TbCourseSession>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbCourseSession> Handle(GetOneCourseSessionQuery request, CancellationToken ct)
        {
            return await _db.TbCourseSessions
                .Include(cs => cs.School)
                .Include(cs => cs.LicenseType)
                .Include(cs => cs.Instructor)
                .FirstOrDefaultAsync(x => x.SessionId == request.SessionId, ct);
        }
    }
}
