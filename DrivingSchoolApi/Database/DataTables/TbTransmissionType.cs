using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Transmission_Type")]

public class TransmissionType
{
    [Key]
    public int TransmissionTypeId { get; set; }

    [Required]
    [MaxLength(50)]
    public string TransmissionTypeName { get; set; }

    // Navigation Properties
    public virtual ICollection<Vehicle> Vehicles { get; set; }
}