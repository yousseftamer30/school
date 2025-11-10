using DrivingSchoolApi.Database.DataSeeder;
//using DrivingSchoolApi.Database.DataTables;
using DrivingSchoolApi.Database.Entities;
using DrivingSchoolApi.Shared.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace DrivingSchoolApi.Database;

public class DBContextHRsystem : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public DBContextHRsystem(DbContextOptions<DBContextHRsystem> options)
        : base(options)
    {
    }

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
    }



}