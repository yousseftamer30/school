
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Vehicles.DeleteVehicle
{
    public record DeleteVehicleCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteVehicleCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken ct)
        {
            var entity = await _db.TbVehicles.FirstOrDefaultAsync(x => x.VehicleId == request.Id, ct);
            if (entity == null) return false;

            _db.TbVehicles.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
