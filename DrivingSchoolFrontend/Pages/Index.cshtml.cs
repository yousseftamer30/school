using DrivingSchoolFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace DrivingSchoolFrontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public CustomerPaymentViewModel? Customer { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync(string phone, string? nationalId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var apiUrl = $"http://localhost:5000/api/enrollment/search-customer?phone={phone}&nationalId={nationalId}";

        try
        {
            var response = await httpClient.GetFromJsonAsync<ApiResponse<CustomerPaymentViewModel>>(apiUrl);
            if (response != null && response.Success)
            {
                Customer = response.Data;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling the API");
        }

        return Page();
    }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
}
