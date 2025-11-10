using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_License_Type")]

public class LicenseType
{
    [Key]
    public int LicenseId { get; set; }

    [Required]
    [MaxLength(100)]
    public string LicenseName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    // Navigation Properties
    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; }
    public virtual ICollection<CourseSession> CourseSessions { get; set; }
    public virtual ICollection<EmployeeLicenseExpertise> EmployeeExpertises { get; set; }
}