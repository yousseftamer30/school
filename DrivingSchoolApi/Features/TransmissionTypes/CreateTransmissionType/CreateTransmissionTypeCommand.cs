
using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchool.Api.Features.TransmissionTypes.CreateTransmissionType
{
    public record CreateTransmissionTypeCommand(string TransmissionTypeName) : IRequest<TransmissionType>;

    public class Handler : IRequestHandler<CreateTransmissionTypeCommand, TransmissionType>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TransmissionType> Handle(CreateTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = new TransmissionType { TransmissionTypeName = request.TransmissionTypeName };
            _db.TransmissionTypes.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
