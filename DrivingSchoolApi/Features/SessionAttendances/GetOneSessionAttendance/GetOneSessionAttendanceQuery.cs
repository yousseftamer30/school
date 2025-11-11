using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SessionAttendances.GetOneSessionAttendance
{
    public record GetOneSessionAttendanceQuery(int Id) : IRequest<TbSessionAttendance>;

    public class Handler : IRequestHandler<GetOneSessionAttendanceQuery, TbSessionAttendance>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSessionAttendance> Handle(GetOneSessionAttendanceQuery request, CancellationToken ct)
        {
            return await _db.TbSessionAttendances
                .Include(sa => sa.CourseSession)
                .Include(sa => sa.Reservation)
                .FirstOrDefaultAsync(x => x.AttendanceId == request.Id, ct);
        }
    }
}
