using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises.DeleteEmployeeLicenseExpertise
{
    public record DeleteEmployeeLicenseExpertiseCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteEmployeeLicenseExpertiseCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteEmployeeLicenseExpertiseCommand request, CancellationToken ct)
        {
            var entity = await _db.EmployeeLicenseExpertises.FirstOrDefaultAsync(x => x.ExpertiseId == request.Id, ct);
            if (entity == null) return false;

            _db.EmployeeLicenseExpertises.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
