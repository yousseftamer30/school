using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Customers.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<List<TbCustomer>>;

    public class Handler : IRequestHandler<GetAllCustomersQuery, List<TbCustomer>>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<List<TbCustomer>> Handle(GetAllCustomersQuery request, CancellationToken ct)
        {
            return await _db.TbCustomers
                .Include(c => c.Reservations)
                .ToListAsync(ct);
        }
    }
}
