namespace DrivingSchoolFrontend.Models;

public class SchoolViewModel
{
    public int SchoolId { get; set; }
    public string Name { get; set; }
    public string? Governorate { get; set; }
}

public class WorkingHourViewModel
{
    public int Id { get; set; }
    public string Day { get; set; }
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }
}

public class InstructorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
