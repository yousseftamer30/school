
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.Schools.UpdateSchool
{
    public record UpdateSchoolCommand(
        int Id,
        string SchoolName,
        string? Location,
        int TotalLectureHalls,
        int SeatsPerHall
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateSchoolCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateSchoolCommand request, CancellationToken ct)
        {
            var entity = await _db.TbSchools.FirstOrDefaultAsync(x => x.SchoolId == request.Id, ct);
            if (entity == null) return false;

            entity.SchoolName = request.SchoolName;
            entity.Location = request.Location;
            entity.TotalLectureHalls = request.TotalLectureHalls;
            entity.SeatsPerHall = request.SeatsPerHall;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
