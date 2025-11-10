
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.Schools.CreateSchool
{
    public record CreateSchoolCommand(
        string SchoolName,
        string? Location,
        int TotalLectureHalls,
        int SeatsPerHall
    ) : IRequest<School>;

    public class Handler : IRequestHandler<CreateSchoolCommand, School>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<School> Handle(CreateSchoolCommand request, CancellationToken ct)
        {
            var entity = new School
            {
                SchoolName = request.SchoolName,
                Location = request.Location,
                TotalLectureHalls = request.TotalLectureHalls,
                SeatsPerHall = request.SeatsPerHall
            };

            _db.Schools.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
