using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.SessionAttendances.GetAllSessionAttendances
{
    public record GetAllSessionAttendancesQuery() : IRequest<List<SessionAttendance>>;

    public class Handler : IRequestHandler<GetAllSessionAttendancesQuery, List<SessionAttendance>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<SessionAttendance>> Handle(GetAllSessionAttendancesQuery request, CancellationToken ct)
        {
            return await _db.SessionAttendances
                .Include(sa => sa.CourseSession)
                .Include(sa => sa.Reservation)
                .ToListAsync(ct);
        }
    }
}
