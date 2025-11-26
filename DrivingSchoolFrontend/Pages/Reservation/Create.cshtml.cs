using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DrivingSchoolFrontend.Models;

namespace DrivingSchoolFrontend.Pages.Reservation
{
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string NationalId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumber { get; set; }

        public string CustomerName { get; set; }
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }

        public List<School> AvailableSchools { get; set; }

        [BindProperty]
        public int SelectedSchoolId { get; set; }

        [BindProperty]
        public string SelectedCourseType { get; set; }

        [BindProperty]
        public DateOnly SelectedDay { get; set; }

        [BindProperty]
        public string SelectedTimeSlot { get; set; }


        public void OnGet()
        {
            // TODO: Call API to get customer and payment details based on NationalId and PhoneNumber.
            // TODO: Call API to get available schools based on the license type from payment details.

            // Dummy data for now:
            CustomerName = "John Doe";
            PaymentId = "PAY12345";
            Amount = 500.00m;
            PaymentStatus = "Unused";
            AvailableSchools = new List<School>
            {
                new School { Id = 1, Name = "Main Street Driving School" },
                new School { Id = 2, Name = "City Center Driving School" }
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Call API to create the reservation.

            return RedirectToPage("/Assignments/Select", new { reservationId = "RES98765" });
        }
    }
}
