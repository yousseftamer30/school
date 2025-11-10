
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.GetOneVehicle
{
    public record GetOneVehicleQuery(int Id) : IRequest<Vehicle?>;

    public class Handler : IRequestHandler<GetOneVehicleQuery, Vehicle?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Vehicle?> Handle(GetOneVehicleQuery request, CancellationToken ct)
        {
            return await _db.Vehicles
                .Include(x => x.LicenseType)
                .Include(x => x.TransmissionType)
                .Include(x => x.School)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.VehicleId == request.Id, ct);
        }
    }
}
