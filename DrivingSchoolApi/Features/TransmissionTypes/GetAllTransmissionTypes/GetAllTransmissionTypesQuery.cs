
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.GetAllTransmissionTypes
{
    public record GetAllTransmissionTypesQuery() : IRequest<List<TbTransmissionType>>;

    public class Handler : IRequestHandler<GetAllTransmissionTypesQuery, List<TbTransmissionType>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbTransmissionType>> Handle(GetAllTransmissionTypesQuery request, CancellationToken ct)
        {
            return await _db.TbTransmissionTypes.AsNoTracking().ToListAsync(ct);
        }
    }
}
