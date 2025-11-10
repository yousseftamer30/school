using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SessionAttendances.DeleteSessionAttendance
{
    public record DeleteSessionAttendanceCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteSessionAttendanceCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = await _db.SessionAttendances.FirstOrDefaultAsync(x => x.AttendanceId == request.Id, ct);
            if (entity == null) return false;

            _db.SessionAttendances.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
