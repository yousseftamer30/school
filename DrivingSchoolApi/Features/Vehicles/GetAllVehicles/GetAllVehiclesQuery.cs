
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.GetAllVehicles
{
    public record GetAllVehiclesQuery() : IRequest<List<TbVehicle>>;

    public class Handler : IRequestHandler<GetAllVehiclesQuery, List<TbVehicle>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbVehicle>> Handle(GetAllVehiclesQuery request, CancellationToken ct)
        {
            return await _db.TbVehicles
                .Include(x => x.LicenseType)
                .Include(x => x.TransmissionType)
                .Include(x => x.School)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
