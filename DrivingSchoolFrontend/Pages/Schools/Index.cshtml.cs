using Microsoft.AspNetCore.Mvc.RazorPages;
using DrivingSchoolFrontend.Models;

namespace DrivingSchoolFrontend.Pages.Schools
{
    public class IndexModel : PageModel
    {
        public List<School> Schools { get; set; }

        public void OnGet()
        {
            // TODO: Call API to get all schools.

            // Dummy data for now:
            Schools = new List<School>
            {
                new School { Id = 1, Name = "Main Street Driving School" },
                new School { Id = 2, Name = "City Center Driving School" },
                new School { Id = 3, Name = "Suburban Driving School" }
            };
        }
    }
}
