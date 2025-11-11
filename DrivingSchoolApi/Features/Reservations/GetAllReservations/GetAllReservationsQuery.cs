using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Reservations.GetAllReservations
{
    public record GetAllReservationsQuery() : IRequest<List<TbReservation>>;

    public class Handler : IRequestHandler<GetAllReservationsQuery, List<TbReservation>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbReservation>> Handle(GetAllReservationsQuery request, CancellationToken ct)
        {
            return await _db.TbReservations
                .Include(r => r.Customer)
                .Include(r => r.LicenseType)
                .Include(r => r.School)
                .ToListAsync(ct);
        }
    }
}
