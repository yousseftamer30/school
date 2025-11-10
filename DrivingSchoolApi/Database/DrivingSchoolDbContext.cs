using DrivingSchoolApi.Database.DataSeeder;
using DrivingSchoolApi.Database.DataTables;

//using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Database.Entities;
using DrivingSchoolApi.Shared.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace DrivingSchoolApi.Database;

public class DrivingSchoolDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DrivingSchoolDbContext(DbContextOptions<DrivingSchoolDbContext> options)
        : base(options)
    {
    }
    // DbSets
    public DbSet<School> Schools { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TransmissionType> TransmissionTypes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<CourseSession> CourseSessions { get; set; }
    public DbSet<SessionAttendance> SessionAttendances { get; set; }
    public DbSet<EmployeeLicenseExpertise> EmployeeLicenseExpertises { get; set; }

    public virtual DbSet<AspPermission> AspPermissions { get; set; }
    public virtual DbSet<AspRolePermissions> AspRolePermissions { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply Identity configurations
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

        // 🔹 Global case-insensitive JSON options
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        // 🔹 Helper for localized data conversion
        ValueConverter<LocalizedData, string> LocalizedConverter =
            new ValueConverter<LocalizedData, string>(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<LocalizedData>(v, jsonOptions)!
            );

        

      
        //modelBuilder.Entity<TbShift>(entity =>
        //{
        //    entity.Property(e => e.ShiftName)
        //          .HasConversion(LocalizedConverter)
        //          .HasColumnType("json");
        //});

        
        // 🔹 Composite key for Role-Permission
        modelBuilder.Entity<AspRolePermissions>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<AspRolePermissions>()
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<AspRolePermissions>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);

        // 🔹 Unique index on permission names
        modelBuilder.Entity<AspPermission>()
            .HasIndex(p => p.PermissionName)
            .IsUnique();



        // ==================== INDEXES ====================

        // Unique Constraints
        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.PlateNumber)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.Phone)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.NationalId)
            .IsUnique();

        // Composite Index for Employee Expertise (prevent duplicates)
        modelBuilder.Entity<EmployeeLicenseExpertise>()
            .HasIndex(e => new { e.EmployeeId, e.LicenseId })
            .IsUnique();

        // Performance Indexes
        modelBuilder.Entity<Reservation>()
            .HasIndex(r => new { r.CustomerId, r.Status });

        modelBuilder.Entity<Reservation>()
            .HasIndex(r => new { r.SchoolId, r.LicenseId });

        modelBuilder.Entity<CourseSession>()
            .HasIndex(cs => new { cs.SchoolId, cs.LicenseId, cs.SessionType });

        modelBuilder.Entity<SessionAttendance>()
            .HasIndex(sa => new { sa.ReservationId, sa.AttendanceDate });

        // ==================== RELATIONSHIPS ====================

        // School -> Employees (1:M)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.School)
            .WithMany(s => s.Employees)
            .HasForeignKey(e => e.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // Role -> Employees (1:M)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // School -> Vehicles (1:M)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.School)
            .WithMany(s => s.Vehicles)
            .HasForeignKey(v => v.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // TransmissionType -> Vehicles (1:M)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.TransmissionType)
            .WithMany(t => t.Vehicles)
            .HasForeignKey(v => v.TransmissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> Vehicles (1:M)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.LicenseType)
            .WithMany(l => l.Vehicles)
            .HasForeignKey(v => v.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer -> Reservations (1:M)
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // LicenseType -> Reservations (1:M)
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.LicenseType)
            .WithMany(l => l.Reservations)
            .HasForeignKey(r => r.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // School -> Reservations (1:M)
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.School)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        // CourseSession Relationships
        modelBuilder.Entity<CourseSession>()
            .HasOne(cs => cs.School)
            .WithMany(s => s.CourseSessions)
            .HasForeignKey(cs => cs.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseSession>()
            .HasOne(cs => cs.LicenseType)
            .WithMany(l => l.CourseSessions)
            .HasForeignKey(cs => cs.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CourseSession>()
            .HasOne(cs => cs.Instructor)
            .WithMany(e => e.CourseSessions)
            .HasForeignKey(cs => cs.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // SessionAttendance Relationships
        modelBuilder.Entity<SessionAttendance>()
            .HasOne(sa => sa.CourseSession)
            .WithMany(cs => cs.SessionAttendances)
            .HasForeignKey(sa => sa.SessionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SessionAttendance>()
            .HasOne(sa => sa.Reservation)
            .WithMany(r => r.SessionAttendances)
            .HasForeignKey(sa => sa.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        // EmployeeLicenseExpertise Relationships
        modelBuilder.Entity<EmployeeLicenseExpertise>()
            .HasOne(ele => ele.Employee)
            .WithMany(e => e.LicenseExpertises)
            .HasForeignKey(ele => ele.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeLicenseExpertise>()
            .HasOne(ele => ele.LicenseType)
            .WithMany(l => l.EmployeeExpertises)
            .HasForeignKey(ele => ele.LicenseId)
            .OnDelete(DeleteBehavior.Restrict);

        // ==================== DEFAULT VALUES ====================

        modelBuilder.Entity<Reservation>()
            .Property(r => r.ReservationDate)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Employee>()
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }



}