using DrivingSchoolApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Employees.GetInstructorsByLicenseAndSchool;

// ============== QUERY ==============
public record GetInstructorsByLicenseAndSchoolQuery : IRequest<List<InstructorDto>>
{
    public int SchoolId { get; init; }
    public int LicenseId { get; init; }
    public bool? CanTeachTheory { get; init; }
    public bool? CanTeachPractical { get; init; }
}

// ============== DTO ==============
public record InstructorDto
{
    public int EmployeeId { get; init; }
    public string EmployeeName { get; init; }
    public int SchoolId { get; init; }
    public string SchoolName { get; init; }
    public int RoleId { get; init; }
    public string RoleName { get; init; }
    public bool IsActive { get; init; }
    public List<LicenseExpertiseDto> LicenseExpertises { get; init; }
}

public record LicenseExpertiseDto
{
    public int ExpertiseId { get; init; }
    public int LicenseGroupId { get; init; }
    public string LicenseGroupName { get; init; }
    public bool CanTeachTheory { get; init; }
    public bool CanTeachPractical { get; init; }
    public DateTime? CertificationDate { get; init; }
    public List<string> LicenseNames { get; init; }
}

// ============== HANDLER ==============
public class Handler : IRequestHandler<GetInstructorsByLicenseAndSchoolQuery, List<InstructorDto>>
{
    private readonly DrivingSchoolDbContext _context;

    public Handler(DrivingSchoolDbContext context)
    {
        _context = context;
    }

    public async Task<List<InstructorDto>> Handle(
        GetInstructorsByLicenseAndSchoolQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.TbEmployees
            .Include(e => e.School)
            .Include(e => e.Role)
            .Include(e => e.LicenseExpertises)
                .ThenInclude(le => le.LicenseGroup)
                    .ThenInclude(lg => lg.LicenseGroupMembers)
                        .ThenInclude(lgm => lgm.LicenseType)
            .Where(e => e.SchoolId == request.SchoolId && e.IsActive)
            .Where(e => e.LicenseExpertises.Any(le =>
                le.LicenseGroup.LicenseGroupMembers.Any(lgm =>
                    lgm.LicenseId == request.LicenseId
                )
            ));

        // Filter by teaching capabilities if specified
        if (request.CanTeachTheory.HasValue)
        {
            query = query.Where(e => e.LicenseExpertises.Any(le =>
                le.CanTeachTheory == request.CanTeachTheory.Value &&
                le.LicenseGroup.LicenseGroupMembers.Any(lgm => lgm.LicenseId == request.LicenseId)
            ));
        }

        //if (request.CanTeachPractical.HasValue)
        //{
        //    query = query.Where(e => e.LicenseExpertises.Any(le =>
        //        le.CanTeachPractical == request.CanTeachPractical.Value &&
        //        le.LicenseGroup.LicenseGroupMembers.Any(lgm => lgm.LicenseId == request.LicenseId)
        //    ));
        //}

        var employees = await query.ToListAsync(cancellationToken);

        return employees.Select(e => new InstructorDto
        {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.EmployeeName,
            SchoolId = e.SchoolId,
            SchoolName = e.School.SchoolName,
            RoleId = e.RoleId,
            RoleName = e.Role.RoleName,
            IsActive = e.IsActive,
            LicenseExpertises = e.LicenseExpertises
                .Where(le => le.LicenseGroup.LicenseGroupMembers.Any(lgm => lgm.LicenseId == request.LicenseId))
                .Select(le => new LicenseExpertiseDto
                {
                    ExpertiseId = le.ExpertiseId,
                    LicenseGroupId = le.LicenseGroupId,
                    LicenseGroupName = le.LicenseGroup.GroupName,
                    CanTeachTheory = le.CanTeachTheory,
                    CanTeachPractical = le.CanTeachPractical,
                    CertificationDate = le.CertificationDate,
                    LicenseNames = le.LicenseGroup.LicenseGroupMembers
                        .Select(lgm => lgm.LicenseType.LicenseName)
                        .ToList()
                }).ToList()
        }).ToList();
    }
}