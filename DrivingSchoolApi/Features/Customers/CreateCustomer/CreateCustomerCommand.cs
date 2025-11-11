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
    ) : IRequest<TbCustomer>;

    public class Handler : IRequestHandler<CreateCustomerCommand, TbCustomer>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbCustomer> Handle(CreateCustomerCommand request, CancellationToken ct)
        {
            var entity = new TbCustomer
            {
                FullName = request.FullName,
                Phone = request.Phone,
                NationalId = request.NationalId,
                Email = request.Email
            };

            _db.TbCustomers.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }
    }
}
