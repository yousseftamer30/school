using DrivingSchoolApi.Features.Customers.CreateCustomer;
using DrivingSchoolApi.Features.Customers.UpdateCustomer;
using DrivingSchoolApi.Features.Customers.GetAllCustomers;
using DrivingSchoolApi.Features.Customers.GetOneCustomer;
using DrivingSchoolApi.Features.Customers.DeleteCustomer;
using MediatR;

namespace DrivingSchoolApi.Features.Customers
{
    public static class CustomerRoutes
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/customers");

            // CREATE
            group.MapPost("/", async (CreateCustomerCommand cmd, ISender mediator) =>
            {
                var result = await mediator.Send(cmd);
                return Results.Ok(new { Success = true, Data = result });
            });

            // UPDATE
            group.MapPut("/{id}", async (int id, UpdateCustomerCommand cmd, ISender mediator) =>
            {
                if (id != cmd.CustomerId)
                    return Results.BadRequest(new { Success = false, Message = "Id mismatch" });

                var success = await mediator.Send(cmd);
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Customer not found" });
            });

            // GET ALL
            group.MapGet("/", async (ISender mediator) =>
            {
                var result = await mediator.Send(new GetAllCustomersQuery());
                return Results.Ok(new { Success = true, Data = result });
            });

            // GET ONE
            group.MapGet("/{id}", async (int id, ISender mediator) =>
            {
                var result = await mediator.Send(new GetOneCustomerQuery(id));
                return result == null
                    ? Results.NotFound(new { Success = false, Message = "Customer not found" })
                    : Results.Ok(new { Success = true, Data = result });
            });

            // DELETE
            group.MapDelete("/{id}", async (int id, ISender mediator) =>
            {
                var success = await mediator.Send(new DeleteCustomerCommand(id));
                return success
                    ? Results.Ok(new { Success = true })
                    : Results.NotFound(new { Success = false, Message = "Customer not found" });
            });
        }
    }
}
