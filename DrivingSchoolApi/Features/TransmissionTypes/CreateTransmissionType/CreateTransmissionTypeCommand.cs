
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.TransmissionTypes.CreateTransmissionType
{
    public record CreateTransmissionTypeCommand(string TransmissionTypeName) : IRequest<TbTransmissionType>;

    public class Handler : IRequestHandler<CreateTransmissionTypeCommand, TbTransmissionType>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbTransmissionType> Handle(CreateTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = new TbTransmissionType { TransmissionTypeName = request.TransmissionTypeName };
            _db.TbTransmissionTypes.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
