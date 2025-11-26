//using DrivingSchoolApi.Features.Employees.GetInstructorsByLicenseAndSchool;
//using MediatR;

//namespace DrivingSchoolApi.Features.Employees;

//public static class InstructorEndpoints
//{
//    public static void MapInstructorEndpoints(this IEndpointRouteBuilder app)
//    {
//        var group = app.MapGroup("/api/instructors");

//        // GET: /api/instructors/by-school-and-license?schoolId=1&licenseId=2&canTeachTheory=true&canTeachPractical=false
//        group.MapGet("/by-school-and-license", async (
//            int schoolId,
//            int licenseId,
//            bool? canTeachTheory,
//            bool? canTeachPractical,
//            ISender mediator) =>
//        {
//            var query = new GetInstructorsByLicenseAndSchoolQuery
//            {
//                SchoolId = schoolId,
//                LicenseId = licenseId,
//                CanTeachTheory = canTeachTheory,
//                CanTeachPractical = canTeachPractical
//            };

//            var result = await mediator.Send(query);
//            return Results.Ok(new { Success = true, Data = result });
//        })
//        .WithName("GetInstructorsBySchoolAndLicense")
//        .WithTags("Instructors")
//        .Produces<object>(200);
//    }
//}

using DrivingSchoolApi.Features.Employees.GetInstructorsByLicenseAndSchool;
using MediatR;

namespace DrivingSchoolApi.Features.Instructors;

public static class InstructorEndpoints
{
    public static void MapInstructorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/instructors")
                       .WithTags("Instructors");

        group.MapGet("/by-school-and-license", async (
            int schoolId,
            int licenseId,
            bool? canTeachTheory,
            bool? canTeachPractical,
            ISender mediator) =>
        {
            var query = new GetInstructorsByLicenseAndSchoolQuery
            {
                SchoolId = schoolId,
                LicenseId = licenseId,
                CanTeachTheory = canTeachTheory,
                CanTeachPractical = canTeachPractical
            };

            var result = await mediator.Send(query);

            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("GetInstructorsBySchoolAndLicense")
        .Produces<object>(200);
    }
}
