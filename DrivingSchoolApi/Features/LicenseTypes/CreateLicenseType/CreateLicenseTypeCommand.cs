
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.LicenseTypes.CreateLicenseType
{
    public record CreateLicenseTypeCommand(string LicenseName, string? Description) : IRequest<TbLicenseType>;

    public class Handler : IRequestHandler<CreateLicenseTypeCommand, TbLicenseType>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbLicenseType> Handle(CreateLicenseTypeCommand request, CancellationToken ct)
        {
            var entity = new TbLicenseType
            {
                LicenseName = request.LicenseName,
                Description = request.Description
            };

            _db.TbLicenseTypes.Add(entity);
            await _db.SaveChangesAsync(ct);

            return entity;
        }
    }
}
