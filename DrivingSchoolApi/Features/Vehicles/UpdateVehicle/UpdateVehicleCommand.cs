
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.UpdateVehicle
{
    public record UpdateVehicleCommand(
        int VehicleId,
        string PlateNumber,
        int SchoolId,
        int TransmissionId,
        int LicenseId,
        bool HasControlPedals,
        string? Notes
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateVehicleCommand request, CancellationToken ct)
        {
            var entity = await _db.TbVehicles.FirstOrDefaultAsync(x => x.VehicleId == request.VehicleId, ct);
            if (entity == null) return false;

            entity.PlateNumber = request.PlateNumber;
            entity.SchoolId = request.SchoolId;
            entity.TransmissionId = request.TransmissionId;
            entity.LicenseId = request.LicenseId;
            entity.HasControlPedals = request.HasControlPedals;
            entity.Notes = request.Notes;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
