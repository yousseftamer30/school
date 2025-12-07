using DrivingSchoolFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace DrivingSchoolFrontend.Pages;

public class SelectSchoolModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SelectSchoolModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty(SupportsGet = true)]
    public int LicenseId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PaymentId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? SchoolId { get; set; }

    public List<SchoolViewModel> Schools { get; set; } = new();
    public List<WorkingHourViewModel> WorkingHours { get; set; } = new();
    public List<InstructorViewModel> Instructors { get; set; } = new();

    public async Task OnGetAsync(string? gov)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var schoolsApiUrl = $"http://localhost:5000/api/enrollment/available-schools?licenseId={LicenseId}";
        if (!string.IsNullOrEmpty(gov))
        {
            schoolsApiUrl += $"&govId={gov}";
        }

        var schoolsResponse = await httpClient.GetFromJsonAsync<ApiResponse<List<SchoolViewModel>>>(schoolsApiUrl);
        if (schoolsResponse != null && schoolsResponse.Success)
        {
            Schools = schoolsResponse.Data;
        }

        if (SchoolId.HasValue)
        {
            var workingHoursApiUrl = $"http://localhost:5000/api/SchoolOperatingHours/today?schoolId={SchoolId.Value}";
            var workingHoursResponse = await httpClient.GetFromJsonAsync<ApiResponse<List<WorkingHourViewModel>>>(workingHoursApiUrl);
            if (workingHoursResponse != null && workingHoursResponse.Success)
            {
                WorkingHours = workingHoursResponse.Data;
            }

            var instructorsApiUrl = $"http://localhost:5000/api/instructors/by-license-and-school?licenseTypeId={LicenseId}&schoolId={SchoolId.Value}";
            var instructorsResponse = await httpClient.GetFromJsonAsync<ApiResponse<List<InstructorViewModel>>>(instructorsApiUrl);
            if (instructorsResponse != null && instructorsResponse.Success)
            {
                Instructors = instructorsResponse.Data;
            }
        }
    }
}
