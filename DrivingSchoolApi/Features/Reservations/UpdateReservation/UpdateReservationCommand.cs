using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.Reservations.UpdateReservation
{
    public record UpdateReservationCommand(
        int ReservationId,
        int CustomerId,
        int LicenseId,
        int SchoolId,
        DateTime ReservationDate,
        ReservationStatus Status,
        string Notes
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken ct)
        {
            var entity = await _db.Reservations.FirstOrDefaultAsync(x => x.ReservationId == request.ReservationId, ct);
            if (entity == null) return false;

            entity.CustomerId = request.CustomerId;
            entity.LicenseId = request.LicenseId;
            entity.SchoolId = request.SchoolId;
            entity.ReservationDate = request.ReservationDate;
            entity.Status = request.Status;
            entity.Notes = request.Notes;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
