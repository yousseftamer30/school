using DrivingSchool.Api.Features.Schools.CreateSchool;
using DrivingSchool.Api.Features.Schools.UpdateSchool;
using DrivingSchool.Api.Features.Schools.GetAllSchools;
using DrivingSchool.Api.Features.Schools.GetOneSchool;
using DrivingSchool.Api.Features.Schools.DeleteSchool;
using MediatR;

namespace DrivingSchool.Api.Features.Schools
{
    public static class SchoolRoutes
    {
        public static void MapSchoolEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/schools");

            // CREATE
            group.MapPost("/", async (CreateSchoolCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateSchoolCommand cmd, ISender mediator) =>
            {
                if (id != cmd.Id)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "School not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllSchoolsQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneSchoolQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "School not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteSchoolCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "School not found" });
            });
        }
    }
}
