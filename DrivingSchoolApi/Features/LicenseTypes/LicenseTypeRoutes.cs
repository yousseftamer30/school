using DrivingSchool.Api.Features.LicenseTypes.CreateLicenseType;
using DrivingSchool.Api.Features.LicenseTypes.UpdateLicenseType;
using DrivingSchool.Api.Features.LicenseTypes.GetAllLicenseTypes;
using DrivingSchool.Api.Features.LicenseTypes.GetOneLicenseType;
using DrivingSchool.Api.Features.LicenseTypes.DeleteLicenseType;
using MediatR;

namespace DrivingSchool.Api.Features.LicenseTypes
{
    public static class LicenseTypeRoutes
    {
        public static void MapLicenseTypeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/license-types");

            // CREATE
            group.MapPost("/", async (CreateLicenseTypeCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateLicenseTypeCommand cmd, ISender mediator) =>
            {
                if (id != cmd.Id)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "LicenseType not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllLicenseTypesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneLicenseTypeQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "LicenseType not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteLicenseTypeCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "LicenseType not found" });
            });
        }
    }
}
