
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.GetOneVehicle
{
    public record GetOneVehicleQuery(int Id) : IRequest<TbVehicle?>;

    public class Handler : IRequestHandler<GetOneVehicleQuery, TbVehicle?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbVehicle?> Handle(GetOneVehicleQuery request, CancellationToken ct)
        {
            return await _db.TbVehicles
                .Include(x => x.LicenseType)
                .Include(x => x.TransmissionType)
                .Include(x => x.School)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.VehicleId == request.Id, ct);
        }
    }
}
