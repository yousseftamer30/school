using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Vehicle")]
public class Vehicle
{
    [Key]
    public int VehicleId { get; set; }

    [Required]
    [MaxLength(50)]
    public string PlateNumber { get; set; }

    [Required]
    public int SchoolId { get; set; }

    [Required]
    public int TransmissionId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    public bool HasControlPedals { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(SchoolId))]
    public virtual School School { get; set; }

    [ForeignKey(nameof(TransmissionId))]
    public virtual TransmissionType TransmissionType { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual LicenseType LicenseType { get; set; }
}