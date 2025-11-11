using DrivingSchoolApi.Database;
using DrivingSchoolApi.Database.DataTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.GetOneEmployee
{
    public record GetOneEmployeeQuery(int EmployeeId) : IRequest<TbEmployee>;

    public class Handler : IRequestHandler<GetOneEmployeeQuery, TbEmployee>
    {
        private readonly DrivingSchoolDbContext _db;
        public Handler(DrivingSchoolDbContext db) => _db = db;

        public async Task<TbEmployee> Handle(GetOneEmployeeQuery request, CancellationToken ct)
        {
            return await _db.TbEmployees
                .Include(e => e.School)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId, ct);
        }
    }
}
