using DrivingSchoolApi.Database;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Reservations.DeleteReservation
{
    public record DeleteReservationCommand(int ReservationId) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken ct)
        {
            var entity = await _db.TbReservations.FirstOrDefaultAsync(x => x.ReservationId == request.ReservationId, ct);
            if (entity == null) return false;

            _db.TbReservations.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
