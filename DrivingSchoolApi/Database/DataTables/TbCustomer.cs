using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DrivingSchoolApi.Database.DataTables;

[Table("Tb_Customer")]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; }

    [MaxLength(20)]
    public string NationalId { get; set; }

    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    // Navigation Properties
    public virtual ICollection<Reservation> Reservations { get; set; }
}