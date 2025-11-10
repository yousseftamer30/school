
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.GetAllVehicles
{
    public record GetAllVehiclesQuery() : IRequest<List<Vehicle>>;

    public class Handler : IRequestHandler<GetAllVehiclesQuery, List<Vehicle>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken ct)
        {
            return await _db.Vehicles
                .Include(x => x.LicenseType)
                .Include(x => x.TransmissionType)
                .Include(x => x.School)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
