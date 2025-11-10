
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Schools.DeleteSchool
{
    public record DeleteSchoolCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteSchoolCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteSchoolCommand request, CancellationToken ct)
        {
            var entity = await _db.Schools.FirstOrDefaultAsync(x => x.SchoolId == request.Id, ct);
            if (entity == null) return false;

            _db.Schools.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
