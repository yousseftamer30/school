using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DrivingSchoolApi.Enums.EnumsList;


namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Reservation")]
public class Reservation
{
    [Key]
    public int ReservationId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    [Required]
    public int SchoolId { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }

    [Required]
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    [MaxLength(1000)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual LicenseType LicenseType { get; set; }

    [ForeignKey(nameof(SchoolId))]
    public virtual School School { get; set; }

    public virtual ICollection<SessionAttendance> SessionAttendances { get; set; }
}