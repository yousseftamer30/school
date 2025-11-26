using DrivingSchoolApi.Features.SessionAttendances.CreateSessionsAfterReservation;
using MediatR;

namespace DrivingSchoolApi.Features.SessionAttendances;

public static class SessionAttendanceAfterReservationEndpoints
{
    public static void MapSessionAttendanceAfterReservationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/session-attendances")
                       .WithTags("Session Attendances");

        group.MapPost("/create-after-reservation", async (
            CreateSessionsAfterReservationCommand cmd,
            ISender mediator) =>
        {
            var result = await mediator.Send(cmd);
            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("CreateSessionsAfterReservation")
        .Produces<object>(200);
    }
}
