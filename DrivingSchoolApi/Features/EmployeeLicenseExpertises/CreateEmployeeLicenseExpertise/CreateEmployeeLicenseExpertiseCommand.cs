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
    ) : IRequest<TbEmployeeLicenseExpertise>;

    public class Handler : IRequestHandler<CreateEmployeeLicenseExpertiseCommand, TbEmployeeLicenseExpertise>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployeeLicenseExpertise> Handle(CreateEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = new TbEmployeeLicenseExpertise
            {
                EmployeeId = request.EmployeeId,
                LicenseId = request.LicenseId,
                CanTeachTheory = request.CanTeachTheory,
                CanTeachPractical = request.CanTeachPractical,
                CertificationDate = request.CertificationDate
            };

            _db.TbEmployeeLicenseExpertises.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
