
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.LicenseTypes.CreateLicenseType
{
    public record CreateLicenseTypeCommand(string LicenseName, string? Description) : IRequest<LicenseType>;

    public class Handler : IRequestHandler<CreateLicenseTypeCommand, LicenseType>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<LicenseType> Handle(CreateLicenseTypeCommand request, CancellationToken ct)
        {
            var entity = new LicenseType
            {
                LicenseName = request.LicenseName,
                Description = request.Description
            };

            _db.LicenseTypes.Add(entity);
            await _db.SaveChangesAsync(ct);

            return entity;
        }
    }
}
