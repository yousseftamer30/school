using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.GetOneTransmissionType
{
    public record GetOneTransmissionTypeQuery(int Id) : IRequest<TbTransmissionType?>;

    public class Handler : IRequestHandler<GetOneTransmissionTypeQuery, TbTransmissionType?>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTransmissionType?> Handle(GetOneTransmissionTypeQuery request, CancellationToken ct)
        {
            return await _db.TbTransmissionTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.TransmissionTypeId == request.Id, ct);
        }
    }
}
