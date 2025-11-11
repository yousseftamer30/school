using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.LicenseTypes.UpdateLicenseType
{
    public record UpdateLicenseTypeCommand(int Id, string LicenseName, string? Description) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateLicenseTypeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateLicenseTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbLicenseTypes.FirstOrDefaultAsync(x => x.LicenseId == request.Id, ct);
            if (entity == null) return false;

            entity.LicenseName = request.LicenseName;
            entity.Description = request.Description;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
