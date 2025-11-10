using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.CourseSessions.DeleteCourseSession
{
    public record DeleteCourseSessionCommand(int SessionId) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteCourseSessionCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteCourseSessionCommand request, CancellationToken ct)
        {
            var entity = await _db.CourseSessions.FirstOrDefaultAsync(x => x.SessionId == request.SessionId, ct);
            if (entity == null) return false;

            _db.CourseSessions.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
