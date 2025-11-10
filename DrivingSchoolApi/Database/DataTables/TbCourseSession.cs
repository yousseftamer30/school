using DrivingSchoolApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;


namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Course_Session")]

public class CourseSession
{
    [Key]
    public int SessionId { get; set; }

    [Required]
    public int SchoolId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    [Required]
    public SessionType SessionType { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal DurationHours { get; set; }

    [Required]
    public int InstructorId { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(SchoolId))]
    public virtual School School { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual LicenseType LicenseType { get; set; }

    [ForeignKey(nameof(InstructorId))]
    public virtual Employee Instructor { get; set; }

    public virtual ICollection<SessionAttendance> SessionAttendances { get; set; }
}