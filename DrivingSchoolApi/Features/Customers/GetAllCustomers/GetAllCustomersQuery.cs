using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Customers.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<List<Customer>>;

    public class Handler : IRequestHandler<GetAllCustomersQuery, List<Customer>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<Customer>> Handle(GetAllCustomersQuery request, CancellationToken ct)
        {
            return await _db.Customers
                .Include(c => c.Reservations)
                .ToListAsync(ct);
        }
    }
}
