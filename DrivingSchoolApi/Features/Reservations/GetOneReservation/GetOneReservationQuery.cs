using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Reservations.GetOneReservation
{
    public record GetOneReservationQuery(int ReservationId) : IRequest<Reservation>;

    public class Handler : IRequestHandler<GetOneReservationQuery, Reservation>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Reservation> Handle(GetOneReservationQuery request, CancellationToken ct)
        {
            return await _db.Reservations
                .Include(r => r.Customer)
                .Include(r => r.LicenseType)
                .Include(r => r.School)
                .FirstOrDefaultAsync(x => x.ReservationId == request.ReservationId, ct);
        }
    }
}
