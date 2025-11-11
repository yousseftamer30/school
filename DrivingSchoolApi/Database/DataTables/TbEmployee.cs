using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Employee")]
public class TbEmployee
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
    public virtual TbSchool School { get; set; }

    [ForeignKey(nameof(RoleId))]
    public virtual TbRole Role { get; set; }

    public virtual ICollection<TbEmployeeLicenseExpertise> LicenseExpertises { get; set; }
    public virtual ICollection<TbCourseSession> CourseSessions { get; set; }
}
