using DrivingSchoolApi.Database;
using DrivingSchoolApi.Features.CallCenterReservation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolApi.Features.Enrollment;

public static class EnrollmentRoutes
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/enrollment")
            .WithTags("Enrollment")
            .WithDescription("إدارة عملية التسجيل والحجز");

        // 🔍 البحث عن العميل والتحقق من الدفعات
        group.MapGet("/search-customer", async (
            string phone,
            string? nationalId,
            ISender mediator) =>
        {
            var query = new SearchCustomerPaymentQuery
            {
                Phone = phone,
                NationalId = nationalId
            };

            var result = await mediator.Send(query);
            if (result == null)
                return Results.NotFound(new { Success = false, Message = "العميل غير موجود" });

            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("SearchCustomerPayment")
        .WithSummary("البحث عن العميل عبر الدفع")
        .Produces<object>(200)
        .Produces<object>(404);

        // 📚 عرض المدارس المتاحة للرخصة
        group.MapGet("/available-schools", async (
            int licenseId,
            int? govId,
            ISender mediator) =>
        {
            var result = await mediator.Send(new GetAvailableSchoolsQuery
            {
                LicenseId = licenseId,
                GovId = govId
            });

            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("EnrollmentAvailableSchools")
        .WithSummary("عرض المدارس المتاحة للرخصة وقت التسجيل")
        .Produces<object>(200);

        // 🧾 إنشاء الحجز وقت التسجيل
        group.MapPost("/create-Customer-reservation", async (
            CreateEnrollmentReservationCommand cmd,
            ISender mediator) =>
        {
            var result = await mediator.Send(cmd);
            return Results.Ok(new { Success = true, Data = result });
        })
        .WithName("EnrollmentCreateReservation")
        .WithSummary("إنشاء حجز أثناء عملية التسجيل")
        .Produces<object>(200);

        // 💳 تفاصيل الدفع
        group.MapGet("/payment-details/{paymentId:int}", async (
            int paymentId,
            DrivingSchoolDbContext context) =>
        {
            var payment = await context.TbPayments
                .Include(p => p.Customer)
                .Include(p => p.LicenseType)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            return payment == null
                ? Results.NotFound(new { Success = false, Message = "عملية الدفع غير موجودة" })
                : Results.Ok(new { Success = true, Data = payment });
        })
        .WithName("EnrollmentPaymentDetails")
        .WithSummary("تفاصيل الدفع أثناء التسجيل")
        .Produces<object>(200)
        .Produces<object>(404);
    }
}
