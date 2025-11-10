using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.CourseSessions.UpdateCourseSession
{
    public record UpdateCourseSessionCommand(
        int SessionId,
        int SchoolId,
        int LicenseId,
        SessionType SessionType,
        decimal DurationHours,
        int InstructorId
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateCourseSessionCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateCourseSessionCommand request, CancellationToken ct)
        {
            var entity = await _db.CourseSessions.FirstOrDefaultAsync(x => x.SessionId == request.SessionId, ct);
            if (entity == null) return false;

            entity.SchoolId = request.SchoolId;
            entity.LicenseId = request.LicenseId;
            entity.SessionType = request.SessionType;
            entity.DurationHours = request.DurationHours;
            entity.InstructorId = request.InstructorId;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
