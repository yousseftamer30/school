using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.CreateEmployeeLicenseExpertise
{
    public record CreateEmployeeLicenseExpertiseCommand(
        int EmployeeId,
        int LicenseId,
        bool CanTeachTheory,
        bool CanTeachPractical,
        DateTime? CertificationDate
    ) : IRequest<EmployeeLicenseExpertise>;

    public class Handler : IRequestHandler<CreateEmployeeLicenseExpertiseCommand, EmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<EmployeeLicenseExpertise> Handle(CreateEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = new EmployeeLicenseExpertise
            {
                EmployeeId = request.EmployeeId,
                LicenseId = request.LicenseId,
                CanTeachTheory = request.CanTeachTheory,
                CanTeachPractical = request.CanTeachPractical,
                CertificationDate = request.CertificationDate
            };

            _db.EmployeeLicenseExpertises.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
