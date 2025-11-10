using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Employee")]
public class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Required]
    [MaxLength(200)]
    public string EmployeeName { get; set; }

    [Required]
    public int SchoolId { get; set; }

    [Required]
    public int RoleId { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation Properties
    [ForeignKey(nameof(SchoolId))]
    public virtual School School { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }

    public virtual ICollection<EmployeeLicenseExpertise> LicenseExpertises { get; set; }
    public virtual ICollection<CourseSession> CourseSessions { get; set; }
}
