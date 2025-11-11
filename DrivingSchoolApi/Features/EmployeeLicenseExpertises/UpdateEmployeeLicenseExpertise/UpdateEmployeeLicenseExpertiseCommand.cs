using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.UpdateEmployeeLicenseExpertise
{
    public record UpdateEmployeeLicenseExpertiseCommand(
        int ExpertiseId,
        int EmployeeId,
        int LicenseId,
        bool CanTeachTheory,
        bool CanTeachPractical,
        DateTime? CertificationDate
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateEmployeeLicenseExpertiseCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = await _db.TbEmployeeLicenseExpertises.FirstOrDefaultAsync(x => x.ExpertiseId == request.ExpertiseId, ct);
            if (entity == null) return false;

            entity.EmployeeId = request.EmployeeId;
            entity.LicenseId = request.LicenseId;
            entity.CanTeachTheory = request.CanTeachTheory;
            entity.CanTeachPractical = request.CanTeachPractical;
            entity.CertificationDate = request.CertificationDate;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
