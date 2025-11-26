using DrivingSchoolApi.Features.CallCenterReservation;
using MediatR;

namespace DrivingSchoolApi.Features.SchoolOperatingHours;

public static class SchoolOperatingHoursReservationEndpoints
{
    public static void MapSchoolOperatingHoursReservationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/schools")
                       .WithTags("School Operating Hours");

        // ====== اليوم الحالي ======
        group.MapGet("/{schoolId:int}/today-hours", async (
            int schoolId,
            ISender mediator) =>
        {
            var result = await mediator.Send(new GetSchoolTodayWorkingHourQuery(schoolId));
            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("GetSchoolTodayWorkingHours")
        .Produces<object>(200);

        // ====== حسب يوم محدد ======
        group.MapGet("/{schoolId:int}/working-hours/{dayOfWeek:int}", async (
            int schoolId,
            int dayOfWeek,
            ISender mediator) =>
        {
            var result = await mediator.Send(new GetSchoolWorkingHourByDayQuery(schoolId, dayOfWeek));
            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("GetSchoolWorkingHoursByDay")
        .Produces<object>(200);
    }
}
