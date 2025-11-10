
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.GetAllTransmissionTypes
{
    public record GetAllTransmissionTypesQuery() : IRequest<List<TransmissionType>>;

    public class Handler : IRequestHandler<GetAllTransmissionTypesQuery, List<TransmissionType>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TransmissionType>> Handle(GetAllTransmissionTypesQuery request, CancellationToken ct)
        {
            return await _db.TransmissionTypes.AsNoTracking().ToListAsync(ct);
        }
    }
}
