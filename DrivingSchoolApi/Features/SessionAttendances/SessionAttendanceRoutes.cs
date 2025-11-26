using DrivingSchoolApi.Features.SessionAttendance;
using MediatR;

namespace DrivingSchoolApi.Features.CallCenterReservation
{
    public static class SessionAttendanceRoutes
    {
        public static void MapSessionAttendanceEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/session-attendances");

            // CREATE
            // CREATE
            group.MapPost("/", async (CreateSessionAttendanceCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateSessionAttendanceCommand cmd, ISender mediator) =>
            {
                if (id != cmd.AttendanceId)
                    return Results.BadRequest(new { Success = false, Message = "ID mismatch" });

                var result = await mediator.Send(cmd);
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Attendance not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new DeleteSessionAttendanceCommand(id));
                return result
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Attendance not found" });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetSessionAttendanceByIdQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Attendance not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllSessionAttendancesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });
        }
    }
}
