using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.SessionAttendances.UpdateSessionAttendance
{
    public record UpdateSessionAttendanceCommand(
        int AttendanceId,
        int SessionId,
        int ReservationId,
        DateTime AttendanceDate,
        AttendanceStatus AttendanceStatus,
        string Notes
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateSessionAttendanceCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = await _db.TbSessionAttendances.FirstOrDefaultAsync(x => x.AttendanceId == request.AttendanceId, ct);
            if (entity == null) return false;

            entity.SessionId = request.SessionId;
            entity.ReservationId = request.ReservationId;
            entity.AttendanceDate = request.AttendanceDate;
            entity.AttendanceStatus = request.AttendanceStatus;
            entity.Notes = request.Notes;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
