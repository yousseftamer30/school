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
            var entity = await _db.TbEmployeeLicenseExpertises.FirstOrDefaultAsync(x => x.ExpertiseId == request.Id, ct);
            if (entity == null) return false;

            _db.TbEmployeeLicenseExpertises.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
