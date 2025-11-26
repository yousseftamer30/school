using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrivingSchoolFrontend.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string NationalId { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Call the customer search API and handle the response.

            return RedirectToPage("/Reservation/Create", new { nationalId = NationalId, phoneNumber = PhoneNumber });
        }
    }
}
