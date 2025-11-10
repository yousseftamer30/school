using DrivingSchoolApi.Features.EmployeeLicenseExpertises.CreateEmployeeLicenseExpertise;
using DrivingSchoolApi.Features.EmployeeLicenseExpertises.UpdateEmployeeLicenseExpertise;
using DrivingSchoolApi.Features.EmployeeLicenseExpertises.GetAllEmployeeLicenseExpertises;
using DrivingSchoolApi.Features.EmployeeLicenseExpertises.GetOneEmployeeLicenseExpertise;
using DrivingSchoolApi.Features.EmployeeLicenseExpertises.DeleteEmployeeLicenseExpertise;
using MediatR;

namespace DrivingSchoolApi.Features.EmployeeLicenseExpertises
{
    public static class EmployeeLicenseExpertiseRoutes
    {
        public static void MapEmployeeLicenseExpertiseEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/employee-license-expertises");

            // CREATE
            group.MapPost("/", async (CreateEmployeeLicenseExpertiseCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateEmployeeLicenseExpertiseCommand cmd, ISender mediator) =>
            {
                if (id != cmd.ExpertiseId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "EmployeeLicenseExpertise not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllEmployeeLicenseExpertisesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneEmployeeLicenseExpertiseQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "EmployeeLicenseExpertise not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteEmployeeLicenseExpertiseCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "EmployeeLicenseExpertise not found" });
            });
        }
    }
}
