using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Customers.UpdateCustomer
{
    public record UpdateCustomerCommand(
        int CustomerId,
        string FullName,
        string Phone,
        string NationalId,
        string Email
    ) : IRequest<bool>;

    public class Handler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            var entity = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, ct);
            if (entity == null) return false;

            entity.FullName = request.FullName;
            entity.Phone = request.Phone;
            entity.NationalId = request.NationalId;
            entity.Email = request.Email;

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
