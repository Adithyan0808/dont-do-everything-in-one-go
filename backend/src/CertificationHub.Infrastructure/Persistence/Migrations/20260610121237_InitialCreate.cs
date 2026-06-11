using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CertificationHub.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificationDrives",
                columns: table => new
                {
                    DriveId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriveCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DriveName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    VendorName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CertificationName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    SponsorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    BudgetAllocated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BudgetConsumed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ManagerApprovalRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttemptLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    TenureRequiredDays = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExamWindowStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExamWindowEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TargetCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentRegistrationCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentCertifiedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PolicyUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationDrives", x => x.DriveId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Department = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    BusinessUnit = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    ManagerEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriveId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EntityName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EntityId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ActorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActorName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    BeforeSnapshotJson = table.Column<string>(type: "TEXT", nullable: false),
                    AfterSnapshotJson = table.Column<string>(type: "TEXT", nullable: false),
                    IPAddress = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    UserAgent = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_CertificationDrives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "CertificationDrives",
                        principalColumn: "DriveId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCertifications",
                columns: table => new
                {
                    CertificationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CertificationName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    VendorName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CredentialId = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsExpired = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCertifications", x => x.CertificationId);
                    table.ForeignKey(
                        name: "FK_EmployeeCertifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    SLAStatus = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ReadDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriveId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamTrack = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PreferredSlot = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PriorAttempts = table.Column<int>(type: "INTEGER", nullable: false),
                    ApproverId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApprovalComments = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_CertificationDrives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "CertificationDrives",
                        principalColumn: "DriveId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResults",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Outcome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EvidenceUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SubmittedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResults", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_AssessmentResults_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EligibilityRecords",
                columns: table => new
                {
                    EligibilityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CriteriaJson = table.Column<string>(type: "TEXT", nullable: false),
                    IsEligible = table.Column<bool>(type: "INTEGER", nullable: false),
                    DecisionReason = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    ApproverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityRecords", x => x.EligibilityId);
                    table.ForeignKey(
                        name: "FK_EligibilityRecords_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriveId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Vendor = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    EncryptedCode = table.Column<string>(type: "TEXT", nullable: false),
                    MaskedCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    AssignedToUserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RedeemedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TokenizedLink = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    RegistrationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                    table.ForeignKey(
                        name: "FK_Vouchers_CertificationDrives_DriveId",
                        column: x => x.DriveId,
                        principalTable: "CertificationDrives",
                        principalColumn: "DriveId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vouchers_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vouchers_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CertificationDrives",
                columns: new[] { "DriveId", "AttemptLimit", "BudgetAllocated", "BudgetConsumed", "CertificationName", "CreatedBy", "CreatedDate", "CurrentCertifiedCount", "CurrentRegistrationCount", "DeletedBy", "DeletedDate", "DriveCode", "DriveName", "ExamWindowEndDate", "ExamWindowStartDate", "IsDeleted", "ManagerApprovalRequired", "ModifiedBy", "ModifiedDate", "PolicyUrl", "RegistrationEndDate", "RegistrationStartDate", "SponsorName", "Status", "TargetCount", "TenureRequiredDays", "VendorName" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), 2, 500000m, 65000m, "AWS Certified Solutions Architect - Associate", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 1, 5, "", null, "AWS-SAA-Q3-2026", "AWS Solutions Architect Associate - Q3 2026", new DateTime(2026, 9, 30, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), false, true, "", null, "https://maverick.example.com/policies/aws-saa-q3-2026", new DateTime(2026, 7, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cloud Center of Excellence", "Open", 50, 180, "AWS" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BusinessUnit", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Department", "Email", "EmployeeId", "FullName", "IsActive", "IsDeleted", "LastLoginDate", "Location", "ManagerEmail", "ModifiedBy", "ModifiedDate", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "BFSI", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Platform Engineering", "admin@maverick.com", "MAV-ADMIN-001", "Maverick Administrator", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Bengaluru", "cio@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Admin" },
                    { new Guid("10000000-0000-0000-0000-000000000101"), "BFSI", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Cloud", "arjun.sharma@maverick.com", "MAV-EMP-101", "Arjun Sharma", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Bengaluru", "manager.cloud@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000102"), "Retail", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Data Engineering", "priya.nair@maverick.com", "MAV-EMP-102", "Priya Nair", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Kochi", "manager.data@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000103"), "Healthcare", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Security", "karthik.raman@maverick.com", "MAV-EMP-103", "Karthik Raman", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Chennai", "manager.security@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000104"), "BFSI", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Platform Engineering", "divya.iyer@maverick.com", "MAV-EMP-104", "Divya Iyer", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Pune", "manager.platform@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000105"), "Retail", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Cloud", "rohit.verma@maverick.com", "MAV-EMP-105", "Rohit Verma", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Hyderabad", "manager.cloud@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000106"), "Healthcare", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Data Engineering", "neha.gupta@maverick.com", "MAV-EMP-106", "Neha Gupta", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Gurugram", "manager.data@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000107"), "BFSI", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Security", "sanjay.menon@maverick.com", "MAV-EMP-107", "Sanjay Menon", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Mumbai", "manager.security@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Manager" },
                    { new Guid("10000000-0000-0000-0000-000000000108"), "Retail", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Platform Engineering", "ananya.krishnan@maverick.com", "MAV-EMP-108", "Ananya Krishnan", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Coimbatore", "manager.platform@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000109"), "Healthcare", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Cloud", "vivek.kumar@maverick.com", "MAV-EMP-109", "Vivek Kumar", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Noida", "manager.cloud@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" },
                    { new Guid("10000000-0000-0000-0000-000000000110"), "BFSI", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, "Data Engineering", "meera.reddy@maverick.com", "MAV-EMP-110", "Meera Reddy", true, false, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Bengaluru", "manager.data@maverick.com", "", null, "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq", "Employee" }
                });

            migrationBuilder.InsertData(
                table: "AuditLogs",
                columns: new[] { "AuditLogId", "Action", "ActorId", "ActorName", "AfterSnapshotJson", "BeforeSnapshotJson", "CreatedBy", "CreatedDate", "DriveId", "EntityId", "EntityName", "IPAddress", "ModifiedBy", "ModifiedDate", "Timestamp", "UserAgent" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), "Created", new Guid("10000000-0000-0000-0000-000000000001"), "Maverick Administrator", "{\"status\":\"Open\"}", "{}", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "20000000-0000-0000-0000-000000000001", "CertificationDrive", "127.0.0.1", "", null, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), "SeedData/1.0" },
                    { new Guid("70000000-0000-0000-0000-000000000002"), "Approved", new Guid("10000000-0000-0000-0000-000000000001"), "Maverick Administrator", "{\"status\":\"Approved\"}", "{\"status\":\"Pending\"}", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "40000000-0000-0000-0000-000000000001", "Registration", "127.0.0.1", "", null, new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), "SeedData/1.0" },
                    { new Guid("70000000-0000-0000-0000-000000000003"), "Assigned", new Guid("10000000-0000-0000-0000-000000000001"), "Maverick Administrator", "{\"status\":\"Assigned\"}", "{\"status\":\"Available\"}", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "50000000-0000-0000-0000-000000000001", "Voucher", "127.0.0.1", "", null, new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), "SeedData/1.0" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeCertifications",
                columns: new[] { "CertificationId", "CertificationName", "CreatedBy", "CreatedDate", "CredentialId", "DeletedBy", "DeletedDate", "ExpiryDate", "IsDeleted", "IsExpired", "IssuedDate", "ModifiedBy", "ModifiedDate", "UserId", "VendorName" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "AWS Cloud Practitioner", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "AWS-CP-2025-101", "", null, new DateTime(2028, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("10000000-0000-0000-0000-000000000101"), "AWS" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "Azure Fundamentals", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "AZ-900-2025-102", "", null, new DateTime(2028, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("10000000-0000-0000-0000-000000000102"), "Microsoft" },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "Google Cloud Digital Leader", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "GCDL-2025-103", "", null, new DateTime(2028, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("10000000-0000-0000-0000-000000000103"), "Google Cloud" },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "AWS Cloud Practitioner", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "AWS-CP-2025-104", "", null, new DateTime(2028, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("10000000-0000-0000-0000-000000000104"), "AWS" },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "Azure Fundamentals", "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "AZ-900-2025-105", "", null, new DateTime(2028, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), false, false, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("10000000-0000-0000-0000-000000000105"), "Microsoft" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "NotificationId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "IsRead", "Message", "ModifiedBy", "ModifiedDate", "ReadDate", "SLAStatus", "Type", "UserId" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, false, false, "Registration approved for AWS Solutions Architect Associate - Q3 2026.", "", null, null, "OnTrack", "RegistrationUpdate", new Guid("10000000-0000-0000-0000-000000000101") },
                    { new Guid("60000000-0000-0000-0000-000000000002"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, false, false, "Voucher assigned for your AWS certification exam.", "", null, null, "OnTrack", "VoucherAssigned", new Guid("10000000-0000-0000-0000-000000000104") },
                    { new Guid("60000000-0000-0000-0000-000000000003"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, false, false, "Exam reminder: complete your certification assessment evidence upload.", "", null, null, "OnTrack", "AssessmentResult", new Guid("10000000-0000-0000-0000-000000000105") }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "ApprovalComments", "ApprovalDate", "ApproverId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "DriveId", "ExamTrack", "IsDeleted", "ModifiedBy", "ModifiedDate", "PreferredSlot", "PriorAttempts", "RegisteredDate", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), "Approved for cloud role alignment.", new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("20000000-0000-0000-0000-000000000001"), "Associate", false, "", null, "Weekday Morning", 0, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Approved", new Guid("10000000-0000-0000-0000-000000000101") },
                    { new Guid("40000000-0000-0000-0000-000000000002"), "", null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("20000000-0000-0000-0000-000000000001"), "Associate", false, "", null, "Weekday Morning", 0, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Pending", new Guid("10000000-0000-0000-0000-000000000102") },
                    { new Guid("40000000-0000-0000-0000-000000000003"), "Prerequisite certification not completed.", new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("20000000-0000-0000-0000-000000000001"), "Associate", false, "", null, "Weekday Morning", 0, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Rejected", new Guid("10000000-0000-0000-0000-000000000103") },
                    { new Guid("40000000-0000-0000-0000-000000000004"), "Voucher assigned.", new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("20000000-0000-0000-0000-000000000001"), "Associate", false, "", null, "Weekday Morning", 0, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Approved", new Guid("10000000-0000-0000-0000-000000000104") },
                    { new Guid("40000000-0000-0000-0000-000000000005"), "Certified after assessment submission.", new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000001"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new Guid("20000000-0000-0000-0000-000000000001"), "Associate", false, "", null, "Weekday Morning", 0, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Completed", new Guid("10000000-0000-0000-0000-000000000105") }
                });

            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "VoucherId", "AssignedDate", "AssignedToUserId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "DeliveryDate", "DriveId", "EncryptedCode", "IsDeleted", "IsRead", "MaskedCode", "ModifiedBy", "ModifiedDate", "RedeemedDate", "RegistrationId", "Status", "TokenizedLink", "Value", "Vendor" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000004"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "1B9E507DEDDE74C8C58E48A540306E37BE3ABE1852BC54D978CF3E0C0908F90E", false, false, "AWS-****D1", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "60C31C641EA10E0C8F4429F8C088D4EEE15E2404C7843A94D754A86BB3C07750", false, false, "AWS-****K2", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "16BD663F257BE9F119EB6C7DF6AD9C71C3CC93A2E22D0C8CF182FFA9D280436A", false, false, "AWS-****P7", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000007"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "DF0FCB17522CE66C4D09940CCBA2714E570456263706F7EA4010E9C933C2D2D3", false, false, "AWS-****S4", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000008"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "1F376D8F2980DCCCE9AD030B7E9AA1390B235CA4EFA804E90B23FEFF4C9009F9", false, false, "AWS-****V3", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000009"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "5AA37CDF937F1817B32C47F2AAEE0A695CA8DF83EE387EEBEEEEC478F96C30FC", false, false, "AWS-****Y9", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000010"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "C00940D65A41C8513D1ABB2A666BBAC0E8EF04AEC0C98F5854E666E40D0307DE", false, false, "AWS-****B8", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000011"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "5B906A239C14A46E34110B08E15106DAB9ACAC64306AE51F689B66FB1E91BE24", false, false, "AWS-****E5", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000012"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "6DD07185D344E641497AA94267122CEBFD6475C0732C0E1D54ABABBA2B535A40", false, false, "AWS-****H1", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000013"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "077CF533DCCD875740A9F6A876D2B19FA32AA60CFBA535C96AAB10B3A2E4C5B2", false, false, "AWS-****L4", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000014"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "3D63753D7FAE422D8BFBC39711C263E53415053A01132B6EC1A2FA06B1D37732", false, false, "AWS-****Q3", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000015"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "C197AE0B65249730AEEA0EE9B68CE20BD53227E2360F78876CBF6D7D2CF5EED5", false, false, "AWS-****T6", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000016"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "B7A8903BE01EEF165A788F95EA643D24A5423705CB77A443B8C9B11ACA7076DB", false, false, "AWS-****W2", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000017"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "AE25CB29E3EAD141F2B16F67A4FCB13CF7249D55D596CA8FFB62F45872A2EE39", false, false, "AWS-****Z7", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000018"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "3C075308CCCA78007FB6CD59C40774AC2D8798525483CD444A59BFA40679BEBF", false, false, "AWS-****C6", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000019"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "CA799445714919C5CD3AA2470AB964291C94CC6D9899F86397620B4C55F8222A", false, false, "AWS-****F8", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000020"), null, null, "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "39D9C4D32BCA74CC7F48C6703D6652BC4F1CDBD04035AEA7C6CC3663A0F49B49", false, false, "AWS-****J5", "", null, null, null, "Available", "", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000101"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, null, new Guid("20000000-0000-0000-0000-000000000001"), "03F6F839DA80A71B88E7E86D1C63130D2E306128644A0372F387AEF85565C848", false, false, "AWS-****M9", "", null, null, new Guid("40000000-0000-0000-0000-000000000001"), "Assigned", "https://maverick.example.com/vouchers/1", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000104"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "4FD8657BA5D3B7B59CDB8245D0D50BC87276D2C68C0799D269ED04A53554C89D", false, false, "AWS-****Q2", "", null, null, new Guid("40000000-0000-0000-0000-000000000004"), "Delivered", "https://maverick.example.com/vouchers/2", 15000m, "AWS" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("10000000-0000-0000-0000-000000000105"), "seed", new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), "", null, new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("20000000-0000-0000-0000-000000000001"), "2178EDC879D5701F803790DCDDF0C7EFCFA4D487459B75D63EAFD0A2BBBBBD19", false, true, "AWS-****R5", "", null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000005"), "Redeemed", "https://maverick.example.com/vouchers/3", 15000m, "AWS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_IsDeleted",
                table: "AssessmentResults",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_Outcome",
                table: "AssessmentResults",
                column: "Outcome");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResults_RegistrationId",
                table: "AssessmentResults",
                column: "RegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ActorId",
                table: "AuditLogs",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DriveId",
                table: "AuditLogs",
                column: "DriveId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName_EntityId",
                table: "AuditLogs",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Timestamp",
                table: "AuditLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationDrives_DriveCode",
                table: "CertificationDrives",
                column: "DriveCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificationDrives_IsDeleted",
                table: "CertificationDrives",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationDrives_RegistrationStartDate",
                table: "CertificationDrives",
                column: "RegistrationStartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationDrives_Status",
                table: "CertificationDrives",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationDrives_VendorName",
                table: "CertificationDrives",
                column: "VendorName");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityRecords_IsDeleted",
                table: "EligibilityRecords",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityRecords_RegistrationId",
                table: "EligibilityRecords",
                column: "RegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_CertificationName",
                table: "EmployeeCertifications",
                column: "CertificationName");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_IsDeleted",
                table: "EmployeeCertifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCertifications_UserId",
                table: "EmployeeCertifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsDeleted",
                table: "Notifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_IsRead",
                table: "Notifications",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_DriveId",
                table: "Registrations",
                column: "DriveId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_IsDeleted",
                table: "Registrations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_Status",
                table: "Registrations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId",
                table: "Registrations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_UserId_DriveId",
                table: "Registrations",
                columns: new[] { "UserId", "DriveId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_AssignedToUserId",
                table: "Vouchers",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_DriveId",
                table: "Vouchers",
                column: "DriveId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_EncryptedCode",
                table: "Vouchers",
                column: "EncryptedCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_IsDeleted",
                table: "Vouchers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_RegistrationId",
                table: "Vouchers",
                column: "RegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_Status",
                table: "Vouchers",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentResults");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "EligibilityRecords");

            migrationBuilder.DropTable(
                name: "EmployeeCertifications");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "CertificationDrives");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
