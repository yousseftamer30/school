using DrivingSchool.Api.Features.TransmissionTypes.CreateTransmissionType;
using DrivingSchool.Api.Features.TransmissionTypes.UpdateTransmissionType;
using DrivingSchool.Api.Features.TransmissionTypes.GetAllTransmissionTypes;
using DrivingSchool.Api.Features.TransmissionTypes.GetOneTransmissionType;
using DrivingSchool.Api.Features.TransmissionTypes.DeleteTransmissionType;
using MediatR;

namespace DrivingSchool.Api.Features.TransmissionTypes
{
    public static class TransmissionTypeRoutes
    {
        public static void MapTransmissionTypeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/transmission-types");

            // CREATE
            group.MapPost("/", async (CreateTransmissionTypeCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateTransmissionTypeCommand cmd, ISender mediator) =>
            {
                if (id != cmd.Id)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "TransmissionType not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllTransmissionTypesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneTransmissionTypeQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "TransmissionType not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteTransmissionTypeCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "TransmissionType not found" });
            });
        }
    }
}
