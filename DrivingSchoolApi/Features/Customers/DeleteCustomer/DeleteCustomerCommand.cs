using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Customers.DeleteCustomer
{
    public record DeleteCustomerCommand(int CustomerId) : IRequest<bool>;

    public class Handler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken ct)
        {
            var entity = await _db.TbCustomers.FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, ct);
            if (entity == null) return false;

            _db.TbCustomers.Remove(entity);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
