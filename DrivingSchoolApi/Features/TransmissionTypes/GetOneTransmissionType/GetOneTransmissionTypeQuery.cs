using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.GetOneTransmissionType
{
    public record GetOneTransmissionTypeQuery(int Id) : IRequest<TransmissionType?>;

    public class Handler : IRequestHandler<GetOneTransmissionTypeQuery, TransmissionType?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TransmissionType?> Handle(GetOneTransmissionTypeQuery request, CancellationToken ct)
        {
            return await _db.TransmissionTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TransmissionTypeId == request.Id, ct);
        }
    }
}
