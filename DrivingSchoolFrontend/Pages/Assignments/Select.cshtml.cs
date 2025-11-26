using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DrivingSchoolFrontend.Models;

namespace DrivingSchoolFrontend.Pages.Assignments
{
    public class SelectModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ReservationId { get; set; }

        public List<Instructor> AvailableInstructors { get; set; }

        [BindProperty]
        public int SelectedInstructorId { get; set; }

        public void OnGet()
        {
            // TODO: Call API to get reservation details.
            // TODO: Call API to get available instructors based on school and license type from reservation.

            // Dummy data for now:
            AvailableInstructors = new List<Instructor>
            {
                new Instructor { Id = 1, Name = "Mr. Smith" },
                new Instructor { Id = 2, Name = "Mrs. Jones" }
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: Call API to create session attendances for the customer under the selected instructor.

            return RedirectToPage("/Index");
        }
    }
}
