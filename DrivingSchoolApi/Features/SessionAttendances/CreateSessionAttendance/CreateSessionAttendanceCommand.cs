using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.SessionAttendances.CreateSessionAttendance
{
    public record CreateSessionAttendanceCommand(
        int SessionId,
        int ReservationId,
        DateTime AttendanceDate,
        AttendanceStatus AttendanceStatus,
        string Notes
    ) : IRequest<TbSessionAttendance>;

    public class Handler : IRequestHandler<CreateSessionAttendanceCommand, TbSessionAttendance>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSessionAttendance> Handle(CreateSessionAttendanceCommand request, CancellationToken ct)
        {
            var entity = new TbSessionAttendance
            {
                SessionId = request.SessionId,
                ReservationId = request.ReservationId,
                AttendanceDate = request.AttendanceDate,
                AttendanceStatus = request.AttendanceStatus,
                Notes = request.Notes
            };

            _db.TbSessionAttendances.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
