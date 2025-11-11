
using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Api.Features.TransmissionTypes.DeleteTransmissionType
{
    public record DeleteTransmissionTypeCommand(int Id) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteTransmissionTypeCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteTransmissionTypeCommand request, CancellationToken ct)
        {
            var entity = await _db.TbTransmissionTypes.FirstOrDefaultAsync(x => x.TransmissionTypeId == request.Id, ct);
            if (entity == null) return false;

            _db.TbTransmissionTypes.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
