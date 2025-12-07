using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

namespace DrivingSchoolFrontend.Pages;

public class CreateReservationModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateReservationModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty(SupportsGet = true)]
    public int PaymentId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int SchoolId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int InstructorId { get; set; }

    public bool ReservationCreated { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();

        var reservationCommand = new { paymentId = PaymentId, schoolId = SchoolId };
        var reservationResponse = await httpClient.PostAsJsonAsync("http://localhost:5000/api/enrollment/create-Customer-reservation", reservationCommand);

        if (reservationResponse.IsSuccessStatusCode)
        {
            var reservationContent = await reservationResponse.Content.ReadAsStringAsync();
            var reservationResult = JsonSerializer.Deserialize<ApiResponse<ReservationResult>>(reservationContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (reservationResult != null && reservationResult.Success)
            {
                var sessionsCommand = new { reservationId = reservationResult.Data.ReservationId };
                var sessionsResponse = await httpClient.PostAsJsonAsync("http://localhost:5000/api/SessionAttendance/create-sessions-after-reservation", sessionsCommand);

                if (sessionsResponse.IsSuccessStatusCode)
                {
                    ReservationCreated = true;
                }
            }
        }

        return Page();
    }
}

public class ReservationResult
{
    public int ReservationId { get; set; }
}
