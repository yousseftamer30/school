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
            var group = app.MapGroup("/api/reservations")
                           .WithTags("Reservations");

            // CREATE
            group.MapPost("/", async (CreateReservationCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            })
            .WithName("CreateReservation")
            .WithSummary("إنشاء حجز جديد")
            .Produces<object>(200);

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateReservationCommand cmd, ISender mediator) =>
            {
                if (id != cmd.ReservationId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Reservation not found" });
            })
            .WithName("UpdateReservation")
            .WithSummary("تحديث بيانات حجز")
            .Produces<object>(200)
            .Produces<object>(404);

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllReservationsQuery());
                return Results.Ok(new { Success = true, Data = result });
            })
            .WithName("GetAllReservations")
            .WithSummary("عرض جميع الحجوزات")
            .Produces<object>(200);

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneReservationQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Reservation not found" })
                    : Results.Ok(new { Success = true, Data = result });
            })
            .WithName("GetReservationById")
            .WithSummary("جلب تفاصيل حجز معين")
            .Produces<object>(200)
            .Produces<object>(404);

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteReservationCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Reservation not found" });
            })
            .WithName("DeleteReservation")
            .WithSummary("حذف حجز")
            .Produces<object>(200)
            .Produces<object>(404);
        }
    }
}
