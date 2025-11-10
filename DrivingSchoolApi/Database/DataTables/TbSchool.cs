using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_School")]
public class School
{
    [Key]
    public int SchoolId { get; set; }

    [Required]
    [MaxLength(200)]
    public string SchoolName { get; set; }

    [MaxLength(500)]
    public string Location { get; set; }

    public int TotalLectureHalls { get; set; }

    public int SeatsPerHall { get; set; }

    // Computed Property
    [NotMapped]
    public int TotalCapacity => TotalLectureHalls * SeatsPerHall;

    // Navigation Properties
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<CourseSession> CourseSessions { get; set; }
}
