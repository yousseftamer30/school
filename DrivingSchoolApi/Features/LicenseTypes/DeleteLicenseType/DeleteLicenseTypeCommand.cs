using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.LicenseTypes.DeleteLicenseType
{
    public record DeleteLicenseTypeCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteLicenseTypeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteLicenseTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbLicenseTypes.FirstOrDefaultAsync(x => x.LicenseId == request.Id, ct);
            if (entity == null) return false;

            _db.TbLicenseTypes.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
