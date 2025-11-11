using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Employee_License_Expertise")]

public class TbEmployeeLicenseExpertise
{
    [Key]
    public int ExpertiseId { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public int LicenseId { get; set; }

    public bool CanTeachTheory { get; set; }

    public bool CanTeachPractical { get; set; }

    public DateTime? CertificationDate { get; set; }

    // Navigation Properties
    [ForeignKey(nameof(EmployeeId))]
    public virtual TbEmployee Employee { get; set; }

    [ForeignKey(nameof(LicenseId))]
    public virtual TbLicenseType LicenseType { get; set; }
}
