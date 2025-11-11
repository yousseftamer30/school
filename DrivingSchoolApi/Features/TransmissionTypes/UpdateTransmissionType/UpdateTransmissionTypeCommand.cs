
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.UpdateTransmissionType
{
    public record UpdateTransmissionTypeCommand(int Id, string TransmissionTypeName) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateTransmissionTypeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTransmissionTypes.FirstOrDefaultAsync(x => x.TransmissionTypeId == request.Id, ct);
            if (entity == null) return false;

            entity.TransmissionTypeName = request.TransmissionTypeName;
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
