using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;


namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Session_Attendance")]
public class SessionAttendance
{
    [Key]
    public int AttendanceId { get; set; }

    [Required]
    public int SessionId { get; set; }

    [Required]
    public int ReservationId { get; set; }

    [Required]
    public DateTime AttendanceDate { get; set; }

    [Required]
    public AttendanceStatus AttendanceStatus { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(SessionId))]
    public virtual CourseSession CourseSession { get; set; }

    [ForeignKey(nameof(ReservationId))]
    public virtual Reservation Reservation { get; set; }
}
