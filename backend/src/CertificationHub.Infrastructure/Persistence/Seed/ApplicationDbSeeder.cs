using System.Security.Cryptography;
using System.Text;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CertificationHub.Infrastructure.Persistence.Seed;

public sealed class ApplicationDbSeeder(
    ApplicationDbContext dbContext,
    ILogger<ApplicationDbSeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var now = DateTime.UtcNow;
        var admin = CreateUser(
            Guid.Parse("10000000-0000-0000-0000-000000000001"),
            "MAV-ADMIN-001",
            "Maverick Administrator",
            "admin@maverick.com",
            "Platform Engineering",
            "BFSI",
            "Bengaluru",
            "cio@maverick.com",
            UserRole.Admin);

        var employees = new[]
        {
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000101"), "MAV-EMP-101", "Arjun Sharma", "arjun.sharma@maverick.com", "Cloud", "BFSI", "Bengaluru", "manager.cloud@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000102"), "MAV-EMP-102", "Priya Nair", "priya.nair@maverick.com", "Data Engineering", "Retail", "Kochi", "manager.data@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000103"), "MAV-EMP-103", "Karthik Raman", "karthik.raman@maverick.com", "Security", "Healthcare", "Chennai", "manager.security@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000104"), "MAV-EMP-104", "Divya Iyer", "divya.iyer@maverick.com", "Platform Engineering", "BFSI", "Pune", "manager.platform@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000105"), "MAV-EMP-105", "Rohit Verma", "rohit.verma@maverick.com", "Cloud", "Retail", "Hyderabad", "manager.cloud@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000106"), "MAV-EMP-106", "Neha Gupta", "neha.gupta@maverick.com", "Data Engineering", "Healthcare", "Gurugram", "manager.data@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000107"), "MAV-EMP-107", "Sanjay Menon", "sanjay.menon@maverick.com", "Security", "BFSI", "Mumbai", "manager.security@maverick.com", UserRole.Manager),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000108"), "MAV-EMP-108", "Ananya Krishnan", "ananya.krishnan@maverick.com", "Platform Engineering", "Retail", "Coimbatore", "manager.platform@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000109"), "MAV-EMP-109", "Vivek Kumar", "vivek.kumar@maverick.com", "Cloud", "Healthcare", "Noida", "manager.cloud@maverick.com", UserRole.Employee),
            CreateUser(Guid.Parse("10000000-0000-0000-0000-000000000110"), "MAV-EMP-110", "Meera Reddy", "meera.reddy@maverick.com", "Data Engineering", "BFSI", "Bengaluru", "manager.data@maverick.com", UserRole.Employee)
        };

        var users = new[] { admin }.Concat(employees).ToArray();
        var drive = CreateDrive(now);
        var registrations = CreateRegistrations(drive.DriveId, employees, admin.UserId, now);
        var vouchers = CreateVouchers(drive.DriveId, employees, registrations, now);

        await dbContext.Users.AddRangeAsync(users, cancellationToken);
        await dbContext.CertificationDrives.AddAsync(drive, cancellationToken);
        await dbContext.EmployeeCertifications.AddRangeAsync(CreateCertifications(employees, now), cancellationToken);
        await dbContext.Registrations.AddRangeAsync(registrations, cancellationToken);
        await dbContext.Vouchers.AddRangeAsync(vouchers, cancellationToken);
        await dbContext.Notifications.AddRangeAsync(CreateNotifications(employees, now), cancellationToken);
        await dbContext.AuditLogs.AddRangeAsync(CreateAuditLogs(drive.DriveId, admin, now), cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeded Maverick Certification Hub demo data.");
    }

    private static User CreateUser(
        Guid userId,
        string employeeId,
        string fullName,
        string email,
        string department,
        string businessUnit,
        string location,
        string managerEmail,
        UserRole role)
    {
        var user = new User
        {
            UserId = userId,
            EmployeeId = employeeId,
            FullName = fullName,
            Email = email,
            Department = department,
            BusinessUnit = businessUnit,
            Location = location,
            ManagerEmail = managerEmail,
            Role = role,
            IsActive = true,
            LastLoginDate = DateTime.UtcNow.AddDays(-7),
            CreatedBy = "seed"
        };

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword($"Maverick@{employeeId[^3..]}");
        return user;
    }

    private static CertificationDrive CreateDrive(DateTime now)
    {
        return new CertificationDrive
        {
            DriveId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
            DriveCode = "AWS-SAA-Q3-2026",
            DriveName = "AWS Solutions Architect Associate - Q3 2026",
            VendorName = "AWS",
            CertificationName = "AWS Certified Solutions Architect - Associate",
            SponsorName = "Cloud Center of Excellence",
            BudgetAllocated = 500000,
            BudgetConsumed = 65000,
            ManagerApprovalRequired = true,
            AttemptLimit = 2,
            TenureRequiredDays = 180,
            RegistrationStartDate = new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc),
            RegistrationEndDate = new DateTime(2026, 7, 31, 23, 59, 59, DateTimeKind.Utc),
            ExamWindowStartDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc),
            ExamWindowEndDate = new DateTime(2026, 9, 30, 23, 59, 59, DateTimeKind.Utc),
            TargetCount = 50,
            CurrentRegistrationCount = 5,
            CurrentCertifiedCount = 1,
            PolicyUrl = "https://maverick.example.com/policies/aws-saa-q3-2026",
            Status = DriveStatus.Open,
            CreatedDate = now,
            CreatedBy = "seed"
        };
    }

    private static IReadOnlyList<EmployeeCertification> CreateCertifications(IReadOnlyList<User> employees, DateTime now)
    {
        return
        [
            CreateCertification(employees[0].UserId, "AWS Cloud Practitioner", "AWS", "AWS-CP-2025-101", now.AddMonths(-10), now.AddYears(2)),
            CreateCertification(employees[1].UserId, "Azure Fundamentals", "Microsoft", "AZ-900-2025-102", now.AddMonths(-8), now.AddYears(2)),
            CreateCertification(employees[2].UserId, "Google Cloud Digital Leader", "Google Cloud", "GCDL-2025-103", now.AddMonths(-6), now.AddYears(2)),
            CreateCertification(employees[3].UserId, "AWS Cloud Practitioner", "AWS", "AWS-CP-2025-104", now.AddMonths(-5), now.AddYears(2)),
            CreateCertification(employees[4].UserId, "Azure Fundamentals", "Microsoft", "AZ-900-2025-105", now.AddMonths(-4), now.AddYears(2))
        ];
    }

    private static EmployeeCertification CreateCertification(Guid userId, string name, string vendor, string credentialId, DateTime issuedDate, DateTime expiryDate)
    {
        return new EmployeeCertification
        {
            CertificationId = Guid.NewGuid(),
            UserId = userId,
            CertificationName = name,
            VendorName = vendor,
            CredentialId = credentialId,
            IssuedDate = issuedDate,
            ExpiryDate = expiryDate,
            IsExpired = expiryDate <= DateTime.UtcNow,
            CreatedBy = "seed"
        };
    }

    private static IReadOnlyList<Registration> CreateRegistrations(Guid driveId, IReadOnlyList<User> employees, Guid approverId, DateTime now)
    {
        return
        [
            CreateRegistration(driveId, employees[0].UserId, approverId, RegistrationStatus.Approved, "Approved for cloud role alignment.", now),
            CreateRegistration(driveId, employees[1].UserId, null, RegistrationStatus.Pending, string.Empty, now),
            CreateRegistration(driveId, employees[2].UserId, approverId, RegistrationStatus.Rejected, "Prerequisite certification not completed.", now),
            CreateRegistration(driveId, employees[3].UserId, approverId, RegistrationStatus.Approved, "Voucher assigned.", now),
            CreateRegistration(driveId, employees[4].UserId, approverId, RegistrationStatus.Completed, "Certified after assessment submission.", now)
        ];
    }

    private static Registration CreateRegistration(
        Guid driveId,
        Guid userId,
        Guid? approverId,
        RegistrationStatus status,
        string comments,
        DateTime now)
    {
        return new Registration
        {
            RegistrationId = Guid.NewGuid(),
            DriveId = driveId,
            UserId = userId,
            ExamTrack = "Associate",
            PreferredSlot = "Weekday Morning",
            PriorAttempts = 0,
            ApproverId = approverId,
            ApprovalDate = approverId.HasValue ? now.AddDays(-2) : null,
            ApprovalComments = comments,
            Status = status,
            RegisteredDate = now.AddDays(-5),
            CreatedBy = "seed"
        };
    }

    private static IReadOnlyList<Voucher> CreateVouchers(Guid driveId, IReadOnlyList<User> employees, IReadOnlyList<Registration> registrations, DateTime now)
    {
        var codes = new[]
        {
            "AWS-X7K2M9", "AWS-P4N8Q2", "AWS-L2T9R5", "AWS-B8C3D1", "AWS-H6J4K2",
            "AWS-M9N1P7", "AWS-Q2R8S4", "AWS-T5U7V3", "AWS-W1X6Y9", "AWS-Z4A2B8",
            "AWS-C7D9E5", "AWS-F3G6H1", "AWS-J8K2L4", "AWS-N5P7Q3", "AWS-R9S1T6",
            "AWS-U4V8W2", "AWS-X3Y5Z7", "AWS-A9B1C6", "AWS-D2E4F8", "AWS-G7H3J5"
        };

        return codes.Select((code, index) =>
        {
            var assignedUserId = index switch
            {
                0 => employees[0].UserId,
                1 => employees[3].UserId,
                2 => employees[4].UserId,
                _ => (Guid?)null
            };

            return new Voucher
            {
                VoucherId = Guid.NewGuid(),
                DriveId = driveId,
                Vendor = "AWS",
                EncryptedCode = HashVoucherCode(code),
                MaskedCode = MaskVoucherCode(code),
                Value = 15000,
                Status = index switch
                {
                    0 => VoucherStatus.Assigned,
                    1 => VoucherStatus.Delivered,
                    2 => VoucherStatus.Redeemed,
                    _ => VoucherStatus.Available
                },
                AssignedToUserId = assignedUserId,
                AssignedDate = assignedUserId.HasValue ? now.AddDays(-1) : null,
                DeliveryDate = index is 1 or 2 ? now : null,
                RedeemedDate = index == 2 ? now.AddDays(1) : null,
                TokenizedLink = assignedUserId.HasValue ? $"https://maverick.example.com/vouchers/{Guid.NewGuid():N}" : string.Empty,
                IsRead = index == 2,
                CreatedBy = "seed"
            };
        }).ToArray();
    }

    private static IReadOnlyList<Notification> CreateNotifications(IReadOnlyList<User> employees, DateTime now)
    {
        return
        [
            CreateNotification(employees[0].UserId, "Registration approved for AWS Solutions Architect Associate - Q3 2026.", NotificationType.RegistrationUpdate, now),
            CreateNotification(employees[3].UserId, "Voucher assigned for your AWS certification exam.", NotificationType.VoucherAssigned, now),
            CreateNotification(employees[4].UserId, "Exam reminder: complete your certification assessment evidence upload.", NotificationType.AssessmentResult, now)
        ];
    }

    private static Notification CreateNotification(Guid userId, string message, NotificationType type, DateTime now)
    {
        return new Notification
        {
            NotificationId = Guid.NewGuid(),
            UserId = userId,
            Message = message,
            Type = type,
            SLAStatus = SLAStatus.OnTrack,
            CreatedDate = now,
            CreatedBy = "seed"
        };
    }

    private static IReadOnlyList<AuditLog> CreateAuditLogs(Guid driveId, User admin, DateTime now)
    {
        return
        [
            CreateAuditLog(driveId, "CertificationDrive", driveId.ToString(), "Created", admin, "{}", "{\"status\":\"Open\"}", now.AddDays(-10)),
            CreateAuditLog(driveId, "Registration", "sample-registration", "Approved", admin, "{\"status\":\"Pending\"}", "{\"status\":\"Approved\"}", now.AddDays(-2)),
            CreateAuditLog(driveId, "Voucher", "sample-voucher", "Assigned", admin, "{\"status\":\"Available\"}", "{\"status\":\"Assigned\"}", now.AddDays(-1))
        ];
    }

    private static AuditLog CreateAuditLog(
        Guid driveId,
        string entityName,
        string entityId,
        string action,
        User actor,
        string beforeSnapshot,
        string afterSnapshot,
        DateTime timestamp)
    {
        return new AuditLog
        {
            AuditLogId = Guid.NewGuid(),
            DriveId = driveId,
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            ActorId = actor.UserId,
            ActorName = actor.FullName,
            BeforeSnapshotJson = beforeSnapshot,
            AfterSnapshotJson = afterSnapshot,
            IPAddress = "127.0.0.1",
            UserAgent = "SeedData/1.0",
            Timestamp = timestamp,
            CreatedBy = "seed"
        };
    }

    private static string HashVoucherCode(string code)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(code));
        return Convert.ToHexString(bytes);
    }

    private static string MaskVoucherCode(string code)
    {
        return $"{code[..4]}****{code[^2..]}";
    }
}
