
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.Vehicles.CreateVehicle
{
    public record CreateVehicleCommand(
        string PlateNumber,
        int SchoolId,
        int TransmissionId,
        int LicenseId,
        bool HasControlPedals,
        string? Notes
    ) : IRequest<TbVehicle>;

    public class Handler : IRequestHandler<CreateVehicleCommand, TbVehicle>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbVehicle> Handle(CreateVehicleCommand request, CancellationToken ct)
        {
            var entity = new TbVehicle
            {
                PlateNumber = request.PlateNumber,
                SchoolId = request.SchoolId,
                TransmissionId = request.TransmissionId,
                LicenseId = request.LicenseId,
                HasControlPedals = request.HasControlPedals,
                Notes = request.Notes
            };

            _db.TbVehicles.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
