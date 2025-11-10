using DrivingSchool.Api.Features.Vehicles.CreateVehicle;
using DrivingSchool.Api.Features.Vehicles.UpdateVehicle;
using DrivingSchool.Api.Features.Vehicles.GetAllVehicles;
using DrivingSchool.Api.Features.Vehicles.GetOneVehicle;
using DrivingSchool.Api.Features.Vehicles.DeleteVehicle;
using MediatR;

namespace DrivingSchool.Api.Features.Vehicles
{
    public static class VehicleRoutes
    {
        public static void MapVehicleEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/vehicles");

            // CREATE
            group.MapPost("/", async (CreateVehicleCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateVehicleCommand cmd, ISender mediator) =>
            {
                if (id != cmd.VehicleId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Vehicle not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllVehiclesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneVehicleQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Vehicle not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteVehicleCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Vehicle not found" });
            });
        }
    }
}
