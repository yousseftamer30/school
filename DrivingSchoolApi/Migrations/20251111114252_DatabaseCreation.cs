using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrivingSchoolApi.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RowGuid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    UserFullName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsToChangePassword = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastPasswordChangedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastFailedLoginAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ForceLogout = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FailedLoginCount = table.Column<int>(type: "int", nullable: false),
                    PreferredLanguage = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionVersion = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PermissionCatagory = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionName = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionDescription = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspPermissions", x => x.PermissionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NationalId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Customer", x => x.CustomerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_License_Type",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LicenseName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_License_Type", x => x.LicenseId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Role", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_School",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalLectureHalls = table.Column<int>(type: "int", nullable: false),
                    SeatsPerHall = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_School", x => x.SchoolId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Transmission_Type",
                columns: table => new
                {
                    TransmissionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransmissionTypeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Transmission_Type", x => x.TransmissionTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspRolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspRolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_AspRolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspRolePermissions_AspPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "AspPermissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Tb_Employee_Tb_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Tb_Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Employee_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Tb_Reservation_Tb_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Tb_Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Reservation_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Reservation_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Vehicle",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlateNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    TransmissionId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    HasControlPedals = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Vehicle", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Tb_Vehicle_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Vehicle_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Vehicle_Tb_Transmission_Type_TransmissionId",
                        column: x => x.TransmissionId,
                        principalTable: "Tb_Transmission_Type",
                        principalColumn: "TransmissionTypeId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Course_Session",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    SessionType = table.Column<int>(type: "int", nullable: false),
                    DurationHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Course_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_Employee_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Tb_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Course_Session_Tb_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Tb_School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Employee_License_Expertise",
                columns: table => new
                {
                    ExpertiseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    CanTeachTheory = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanTeachPractical = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CertificationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Employee_License_Expertise", x => x.ExpertiseId);
                    table.ForeignKey(
                        name: "FK_Tb_Employee_License_Expertise_Tb_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Tb_Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Employee_License_Expertise_Tb_License_Type_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Tb_License_Type",
                        principalColumn: "LicenseId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tb_Session_Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AttendanceStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Session_Attendance", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Tb_Session_Attendance_Tb_Course_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Tb_Course_Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tb_Session_Attendance_Tb_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Tb_Reservation",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "0b2f3b2f-a5d1-4d3f-be8b-db04070caed1", "SystemAdmin", "SYSTEMADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "EmployeeId", "FailedLoginCount", "ForceLogout", "IsActive", "IsToChangePassword", "LastFailedLoginAt", "LastLoginAt", "LastPasswordChangedAt", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PermissionVersion", "PhoneNumber", "PhoneNumberConfirmed", "PreferredLanguage", "RowGuid", "SecurityStamp", "TwoFactorEnabled", "UserFullName", "UserName" },
                values: new object[] { 1, 0, 1, "2cc3da7b-b1d4-43fc-b129-4e706e02ac96", new DateTime(2025, 11, 11, 13, 42, 45, 428, DateTimeKind.Local).AddTicks(242), null, "systemadmin@example.com", false, 1, 0, false, true, false, null, null, new DateTime(2025, 11, 11, 11, 42, 45, 428, DateTimeKind.Utc).AddTicks(2546), false, null, "SYSTEMADMIN@EXAMPLE.COM", "BOLES", "AQAAAAIAAYagAAAAEHuYA7U5KAgI1iuzqry/7jPmIBrciy7nyILnyLHLuOwz3plNoiOeAavDPyJliZul9A==", 1, "01200000000", true, "en", new Guid("35195670-1818-4919-b1b9-255fa2e566a1"), "6QVLU2WHQVYOV4FRB6EFKIGE2KJJICGL", false, "Boles Lewis Boles", "Boles" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspPermissions_PermissionName",
                table: "AspPermissions",
                column: "PermissionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspRolePermissions_PermissionId",
                table: "AspRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_InstructorId",
                table: "Tb_Course_Session",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_LicenseId",
                table: "Tb_Course_Session",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Course_Session_SchoolId_LicenseId_SessionType",
                table: "Tb_Course_Session",
                columns: new[] { "SchoolId", "LicenseId", "SessionType" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Customer_NationalId",
                table: "Tb_Customer",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Customer_Phone",
                table: "Tb_Customer",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Employee_RoleId",
                table: "Tb_Employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Employee_SchoolId",
                table: "Tb_Employee",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Employee_License_Expertise_EmployeeId_LicenseId",
                table: "Tb_Employee_License_Expertise",
                columns: new[] { "EmployeeId", "LicenseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Employee_License_Expertise_LicenseId",
                table: "Tb_Employee_License_Expertise",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reservation_CustomerId_Status",
                table: "Tb_Reservation",
                columns: new[] { "CustomerId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reservation_LicenseId",
                table: "Tb_Reservation",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Reservation_SchoolId_LicenseId",
                table: "Tb_Reservation",
                columns: new[] { "SchoolId", "LicenseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_ReservationId_AttendanceDate",
                table: "Tb_Session_Attendance",
                columns: new[] { "ReservationId", "AttendanceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Session_Attendance_SessionId",
                table: "Tb_Session_Attendance",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Vehicle_LicenseId",
                table: "Tb_Vehicle",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Vehicle_PlateNumber",
                table: "Tb_Vehicle",
                column: "PlateNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Vehicle_SchoolId",
                table: "Tb_Vehicle",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Vehicle_TransmissionId",
                table: "Tb_Vehicle",
                column: "TransmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspRolePermissions");

            migrationBuilder.DropTable(
                name: "Tb_Employee_License_Expertise");

            migrationBuilder.DropTable(
                name: "Tb_Session_Attendance");

            migrationBuilder.DropTable(
                name: "Tb_Vehicle");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspPermissions");

            migrationBuilder.DropTable(
                name: "Tb_Course_Session");

            migrationBuilder.DropTable(
                name: "Tb_Reservation");

            migrationBuilder.DropTable(
                name: "Tb_Transmission_Type");

            migrationBuilder.DropTable(
                name: "Tb_Employee");

            migrationBuilder.DropTable(
                name: "Tb_Customer");

            migrationBuilder.DropTable(
                name: "Tb_License_Type");

            migrationBuilder.DropTable(
                name: "Tb_Role");

            migrationBuilder.DropTable(
                name: "Tb_School");
        }
    }
}
