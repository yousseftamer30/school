using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Customers.GetOneCustomer
{
    public record GetOneCustomerQuery(int CustomerId) : IRequest<Customer>;

    public class Handler : IRequestHandler<GetOneCustomerQuery, Customer>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Customer> Handle(GetOneCustomerQuery request, CancellationToken ct)
        {
            return await _db.Customers
                .Include(c => c.Reservations)
                .FirstOrDefaultAsync(x => x.CustomerId == request.CustomerId, ct);
        }
    }
}
