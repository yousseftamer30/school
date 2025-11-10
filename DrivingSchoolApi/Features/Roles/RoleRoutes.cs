using DrivingSchool.Api.Features.Roles.CreateRole;
using DrivingSchool.Api.Features.Roles.UpdateRole;
using DrivingSchool.Api.Features.Roles.GetAllRoles;
using DrivingSchool.Api.Features.Roles.GetOneRole;
using DrivingSchool.Api.Features.Roles.DeleteRole;
using MediatR;

namespace DrivingSchool.Api.Features.Roles
{
    public static class RoleRoutes
    {
        public static void MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/roles");

            group.MapPost("/", async (CreateRoleCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            group.MapPut("/{id}", async (int id, UpdateRoleCommand cmd, ISender mediator) =>
            {
                if (id != cmd.Id)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Role not found" });
            });

            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllRolesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneRoleQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Role not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteRoleCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Role not found" });
            });
        }
    }
}
