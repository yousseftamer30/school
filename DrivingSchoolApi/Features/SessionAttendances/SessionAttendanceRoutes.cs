using DrivingSchoolApi.Features.SessionAttendances.CreateSessionAttendance;
using DrivingSchoolApi.Features.SessionAttendances.UpdateSessionAttendance;
using DrivingSchoolApi.Features.SessionAttendances.GetAllSessionAttendances;
using DrivingSchoolApi.Features.SessionAttendances.GetOneSessionAttendance;
using DrivingSchoolApi.Features.SessionAttendances.DeleteSessionAttendance;
using MediatR;

namespace DrivingSchoolApi.Features.SessionAttendances
{
    public static class SessionAttendanceRoutes
    {
        public static void MapSessionAttendanceEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/session-attendances");

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
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "SessionAttendance not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllSessionAttendancesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneSessionAttendanceQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "SessionAttendance not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteSessionAttendanceCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "SessionAttendance not found" });
            });
        }
    }
}
