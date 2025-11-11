
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
    ) : IRequest<TbSchool>;

    public class Handler : IRequestHandler<CreateSchoolCommand, TbSchool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbSchool> Handle(CreateSchoolCommand request, CancellationToken ct)
        {
            var entity = new TbSchool
            {
                SchoolName = request.SchoolName,
                Location = request.Location,
                TotalLectureHalls = request.TotalLectureHalls,
                SeatsPerHall = request.SeatsPerHall
            };

            _db.TbSchools.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
