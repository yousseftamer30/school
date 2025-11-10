using DrivingSchoolApi.Features.Reservations.CreateReservation;
using DrivingSchoolApi.Features.Reservations.UpdateReservation;
using DrivingSchoolApi.Features.Reservations.GetAllReservations;
using DrivingSchoolApi.Features.Reservations.GetOneReservation;
using DrivingSchoolApi.Features.Reservations.DeleteReservation;
using MediatR;

namespace DrivingSchoolApi.Features.Reservations
{
    public static class ReservationRoutes
    {
        public static void MapReservationEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/reservations");

            // CREATE
            group.MapPost("/", async (CreateReservationCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateReservationCommand cmd, ISender mediator) =>
            {
                if (id != cmd.ReservationId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Reservation not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllReservationsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneReservationQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Reservation not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteReservationCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Reservation not found" });
            });
        }
    }
}
