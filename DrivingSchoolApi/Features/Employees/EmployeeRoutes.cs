using DrivingSchoolApi.Features.Employees.CreateEmployee;
using DrivingSchoolApi.Features.Employees.UpdateEmployee;
using DrivingSchoolApi.Features.Employees.GetAllEmployees;
using DrivingSchoolApi.Features.Employees.GetOneEmployee;
using DrivingSchoolApi.Features.Employees.DeleteEmployee;
using MediatR;

namespace DrivingSchoolApi.Features.Employees
{
    public static class EmployeeRoutes
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/employees");

            // CREATE
            group.MapPost("/", async (CreateEmployeeCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateEmployeeCommand cmd, ISender mediator) =>
            {
                if (id != cmd.EmployeeId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Employee not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllEmployeesQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneEmployeeQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Employee not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteEmployeeCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Employee not found" });
            });
        }
    }
}
