using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using static DrivingSchoolApi.Enums.EnumsList;

namespace DrivingSchoolApi.Features.Reservations.CreateReservation
{
    public record CreateReservationCommand(
        int CustomerId,
        int LicenseId,
        int SchoolId,
        DateTime ReservationDate,
        ReservationStatus Status,
        string Notes
    ) : IRequest<TbReservation>;

    public class Handler : IRequestHandler<CreateReservationCommand, TbReservation>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbReservation> Handle(CreateReservationCommand request, CancellationToken ct)
        {
            var entity = new TbReservation
            {
                CustomerId = request.CustomerId,
                LicenseId = request.LicenseId,
                SchoolId = request.SchoolId,
                ReservationDate = request.ReservationDate,
                Status = request.Status,
                Notes = request.Notes
            };

            _db.TbReservations.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
