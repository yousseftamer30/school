using Microsoft.AspNetCore.Mvc.RazorPages;
using DrivingSchoolFrontend.Models;

namespace DrivingSchoolFrontend.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        public List<Instructor> Instructors { get; set; }

        public void OnGet()
        {
            // TODO: Call API to get all instructors.

            // Dummy data for now:
            Instructors = new List<Instructor>
            {
                new Instructor { Id = 1, Name = "Mr. Smith" },
                new Instructor { Id = 2, Name = "Mrs. Jones" },
                new Instructor { Id = 3, Name = "Mr. Williams" }
            };
        }
    }
}
