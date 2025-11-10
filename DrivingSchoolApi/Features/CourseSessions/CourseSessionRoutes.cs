using DrivingSchoolApi.Features.CourseSessions.CreateCourseSession;
using DrivingSchoolApi.Features.CourseSessions.UpdateCourseSession;
using DrivingSchoolApi.Features.CourseSessions.GetAllCourseSessions;
using DrivingSchoolApi.Features.CourseSessions.GetOneCourseSession;
using DrivingSchoolApi.Features.CourseSessions.DeleteCourseSession;
using MediatR;

namespace DrivingSchoolApi.Features.CourseSessions
{
    public static class CourseSessionRoutes
    {
        public static void MapCourseSessionEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/course-sessions");

            // CREATE
            group.MapPost("/", async (CreateCourseSessionCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateCourseSessionCommand cmd, ISender mediator) =>
            {
                if (id != cmd.SessionId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "CourseSession not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllCourseSessionsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneCourseSessionQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "CourseSession not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteCourseSessionCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "CourseSession not found" });
            });
        }
    }
}
