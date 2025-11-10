using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;

namespace DrivingSchoolApi.Features.Customers.CreateCustomer
{
    public record CreateCustomerCommand(
        string FullName,
        string Phone,
        string NationalId,
        string Email
    ) : IRequest<Customer>;

    public class Handler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken ct)
        {
            var entity = new Customer
            {
                FullName = request.FullName,
                Phone = request.Phone,
                NationalId = request.NationalId,
                Email = request.Email
            };

            _db.Customers.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
