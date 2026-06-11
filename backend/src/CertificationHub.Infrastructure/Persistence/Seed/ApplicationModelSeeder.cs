using System.Security.Cryptography;
using System.Text;
using CertificationHub.Domain.Entities;
using CertificationHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CertificationHub.Infrastructure.Persistence.Seed;

internal static class ApplicationModelSeeder
{
    private static readonly DateTime SeedDate = new(2026, 6, 9, 0, 0, 0, DateTimeKind.Utc);
    private const string SeedUser = "seed";
    private const string SeedPasswordHash = "$2a$11$ZONQLjnpJocHuJeJaJGcEuhwh1kLPJKb1ZC9klf6Vw.KAX1MTW2Gq";
    private static readonly Guid AdminId = Guid.Parse("10000000-0000-0000-0000-000000000001");
    private static readonly Guid DriveId = Guid.Parse("20000000-0000-0000-0000-000000000001");

    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedUsers(modelBuilder);
        SeedCertifications(modelBuilder);
        SeedDrive(modelBuilder);
        SeedRegistrations(modelBuilder);
        SeedVouchers(modelBuilder);
        SeedNotifications(modelBuilder);
        SeedAuditLogs(modelBuilder);
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            User(AdminId, "MAV-ADMIN-001", "Maverick Administrator", "admin@maverick.com", "Platform Engineering", "BFSI", "Bengaluru", "cio@maverick.com", UserRole.Admin),
            User(Guid.Parse("10000000-0000-0000-0000-000000000101"), "MAV-EMP-101", "Arjun Sharma", "arjun.sharma@maverick.com", "Cloud", "BFSI", "Bengaluru", "manager.cloud@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000102"), "MAV-EMP-102", "Priya Nair", "priya.nair@maverick.com", "Data Engineering", "Retail", "Kochi", "manager.data@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000103"), "MAV-EMP-103", "Karthik Raman", "karthik.raman@maverick.com", "Security", "Healthcare", "Chennai", "manager.security@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000104"), "MAV-EMP-104", "Divya Iyer", "divya.iyer@maverick.com", "Platform Engineering", "BFSI", "Pune", "manager.platform@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000105"), "MAV-EMP-105", "Rohit Verma", "rohit.verma@maverick.com", "Cloud", "Retail", "Hyderabad", "manager.cloud@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000106"), "MAV-EMP-106", "Neha Gupta", "neha.gupta@maverick.com", "Data Engineering", "Healthcare", "Gurugram", "manager.data@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000107"), "MAV-EMP-107", "Sanjay Menon", "sanjay.menon@maverick.com", "Security", "BFSI", "Mumbai", "manager.security@maverick.com", UserRole.Manager),
            User(Guid.Parse("10000000-0000-0000-0000-000000000108"), "MAV-EMP-108", "Ananya Krishnan", "ananya.krishnan@maverick.com", "Platform Engineering", "Retail", "Coimbatore", "manager.platform@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000109"), "MAV-EMP-109", "Vivek Kumar", "vivek.kumar@maverick.com", "Cloud", "Healthcare", "Noida", "manager.cloud@maverick.com", UserRole.Employee),
            User(Guid.Parse("10000000-0000-0000-0000-000000000110"), "MAV-EMP-110", "Meera Reddy", "meera.reddy@maverick.com", "Data Engineering", "BFSI", "Bengaluru", "manager.data@maverick.com", UserRole.Employee));
    }

    private static User User(Guid id, string employeeId, string name, string email, string department, string businessUnit, string location, string managerEmail, UserRole role)
    {
        return new User
        {
            UserId = id,
            EmployeeId = employeeId,
            FullName = name,
            Email = email,
            Department = department,
            BusinessUnit = businessUnit,
            Location = location,
            ManagerEmail = managerEmail,
            Role = role,
            PasswordHash = SeedPasswordHash,
            IsActive = true,
            LastLoginDate = SeedDate.AddDays(-7),
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty,
            DeletedBy = string.Empty
        };
    }

    private static void SeedCertifications(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeCertification>().HasData(
            Certification("30000000-0000-0000-0000-000000000001", "10000000-0000-0000-0000-000000000101", "AWS Cloud Practitioner", "AWS", "AWS-CP-2025-101", -10, 24),
            Certification("30000000-0000-0000-0000-000000000002", "10000000-0000-0000-0000-000000000102", "Azure Fundamentals", "Microsoft", "AZ-900-2025-102", -8, 24),
            Certification("30000000-0000-0000-0000-000000000003", "10000000-0000-0000-0000-000000000103", "Google Cloud Digital Leader", "Google Cloud", "GCDL-2025-103", -6, 24),
            Certification("30000000-0000-0000-0000-000000000004", "10000000-0000-0000-0000-000000000104", "AWS Cloud Practitioner", "AWS", "AWS-CP-2025-104", -5, 24),
            Certification("30000000-0000-0000-0000-000000000005", "10000000-0000-0000-0000-000000000105", "Azure Fundamentals", "Microsoft", "AZ-900-2025-105", -4, 24));
    }

    private static EmployeeCertification Certification(string id, string userId, string name, string vendor, string credentialId, int issueOffsetMonths, int expiryOffsetMonths)
    {
        return new EmployeeCertification
        {
            CertificationId = Guid.Parse(id),
            UserId = Guid.Parse(userId),
            CertificationName = name,
            VendorName = vendor,
            CredentialId = credentialId,
            IssuedDate = SeedDate.AddMonths(issueOffsetMonths),
            ExpiryDate = SeedDate.AddMonths(expiryOffsetMonths),
            IsExpired = false,
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty,
            DeletedBy = string.Empty
        };
    }

    private static void SeedDrive(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CertificationDrive>().HasData(new CertificationDrive
        {
            DriveId = DriveId,
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
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty,
            DeletedBy = string.Empty
        });
    }

    private static void SeedRegistrations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>().HasData(
            Registration("40000000-0000-0000-0000-000000000001", "10000000-0000-0000-0000-000000000101", RegistrationStatus.Approved, "Approved for cloud role alignment.", true),
            Registration("40000000-0000-0000-0000-000000000002", "10000000-0000-0000-0000-000000000102", RegistrationStatus.Pending, string.Empty, false),
            Registration("40000000-0000-0000-0000-000000000003", "10000000-0000-0000-0000-000000000103", RegistrationStatus.Rejected, "Prerequisite certification not completed.", true),
            Registration("40000000-0000-0000-0000-000000000004", "10000000-0000-0000-0000-000000000104", RegistrationStatus.Approved, "Voucher assigned.", true),
            Registration("40000000-0000-0000-0000-000000000005", "10000000-0000-0000-0000-000000000105", RegistrationStatus.Completed, "Certified after assessment submission.", true));
    }

    private static Registration Registration(string id, string userId, RegistrationStatus status, string comments, bool approved)
    {
        return new Registration
        {
            RegistrationId = Guid.Parse(id),
            DriveId = DriveId,
            UserId = Guid.Parse(userId),
            ExamTrack = "Associate",
            PreferredSlot = "Weekday Morning",
            PriorAttempts = 0,
            ApproverId = approved ? AdminId : null,
            ApprovalDate = approved ? SeedDate.AddDays(-2) : null,
            ApprovalComments = comments,
            Status = status,
            RegisteredDate = SeedDate.AddDays(-5),
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty,
            DeletedBy = string.Empty
        };
    }

    private static void SeedVouchers(ModelBuilder modelBuilder)
    {
        var codes = new[]
        {
            "AWS-X7K2M9", "AWS-P4N8Q2", "AWS-L2T9R5", "AWS-B8C3D1", "AWS-H6J4K2",
            "AWS-M9N1P7", "AWS-Q2R8S4", "AWS-T5U7V3", "AWS-W1X6Y9", "AWS-Z4A2B8",
            "AWS-C7D9E5", "AWS-F3G6H1", "AWS-J8K2L4", "AWS-N5P7Q3", "AWS-R9S1T6",
            "AWS-U4V8W2", "AWS-X3Y5Z7", "AWS-A9B1C6", "AWS-D2E4F8", "AWS-G7H3J5"
        };

        var vouchers = codes.Select((code, index) => new
        {
            VoucherId = Guid.Parse($"50000000-0000-0000-0000-{index + 1:000000000000}"),
            DriveId,
            Vendor = "AWS",
            EncryptedCode = Hash(code),
            MaskedCode = $"{code[..4]}****{code[^2..]}",
            Value = 15000m,
            Status = index switch
            {
                0 => VoucherStatus.Assigned,
                1 => VoucherStatus.Delivered,
                2 => VoucherStatus.Redeemed,
                _ => VoucherStatus.Available
            },
            AssignedToUserId = index switch
            {
                0 => Guid.Parse("10000000-0000-0000-0000-000000000101"),
                1 => Guid.Parse("10000000-0000-0000-0000-000000000104"),
                2 => Guid.Parse("10000000-0000-0000-0000-000000000105"),
                _ => (Guid?)null
            },
            AssignedDate = index <= 2 ? SeedDate.AddDays(-1) : (DateTime?)null,
            DeliveryDate = index is 1 or 2 ? SeedDate : (DateTime?)null,
            RedeemedDate = index == 2 ? SeedDate.AddDays(1) : (DateTime?)null,
            TokenizedLink = index <= 2 ? $"https://maverick.example.com/vouchers/{index + 1}" : string.Empty,
            IsRead = index == 2,
            RegistrationId = index switch
            {
                0 => Guid.Parse("40000000-0000-0000-0000-000000000001"),
                1 => Guid.Parse("40000000-0000-0000-0000-000000000004"),
                2 => Guid.Parse("40000000-0000-0000-0000-000000000005"),
                _ => (Guid?)null
            },
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedDate = (DateTime?)null,
            ModifiedBy = string.Empty,
            IsDeleted = false,
            DeletedDate = (DateTime?)null,
            DeletedBy = string.Empty
        }).ToArray();

        modelBuilder.Entity<Voucher>().HasData(vouchers);
    }

    private static void SeedNotifications(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>().HasData(
            Notification("60000000-0000-0000-0000-000000000001", "10000000-0000-0000-0000-000000000101", "Registration approved for AWS Solutions Architect Associate - Q3 2026.", NotificationType.RegistrationUpdate),
            Notification("60000000-0000-0000-0000-000000000002", "10000000-0000-0000-0000-000000000104", "Voucher assigned for your AWS certification exam.", NotificationType.VoucherAssigned),
            Notification("60000000-0000-0000-0000-000000000003", "10000000-0000-0000-0000-000000000105", "Exam reminder: complete your certification assessment evidence upload.", NotificationType.AssessmentResult));
    }

    private static Notification Notification(string id, string userId, string message, NotificationType type)
    {
        return new Notification
        {
            NotificationId = Guid.Parse(id),
            UserId = Guid.Parse(userId),
            Message = message,
            Type = type,
            IsRead = false,
            SLAStatus = SLAStatus.OnTrack,
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty,
            DeletedBy = string.Empty
        };
    }

    private static void SeedAuditLogs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>().HasData(
            Audit("70000000-0000-0000-0000-000000000001", "CertificationDrive", DriveId.ToString(), "Created", "{}", "{\"status\":\"Open\"}", SeedDate.AddDays(-10)),
            Audit("70000000-0000-0000-0000-000000000002", "Registration", "40000000-0000-0000-0000-000000000001", "Approved", "{\"status\":\"Pending\"}", "{\"status\":\"Approved\"}", SeedDate.AddDays(-2)),
            Audit("70000000-0000-0000-0000-000000000003", "Voucher", "50000000-0000-0000-0000-000000000001", "Assigned", "{\"status\":\"Available\"}", "{\"status\":\"Assigned\"}", SeedDate.AddDays(-1)));
    }

    private static AuditLog Audit(string id, string entityName, string entityId, string action, string beforeSnapshot, string afterSnapshot, DateTime timestamp)
    {
        return new AuditLog
        {
            AuditLogId = Guid.Parse(id),
            DriveId = DriveId,
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            ActorId = AdminId,
            ActorName = "Maverick Administrator",
            BeforeSnapshotJson = beforeSnapshot,
            AfterSnapshotJson = afterSnapshot,
            IPAddress = "127.0.0.1",
            UserAgent = "SeedData/1.0",
            Timestamp = timestamp,
            CreatedDate = SeedDate,
            CreatedBy = SeedUser,
            ModifiedBy = string.Empty
        };
    }

    private static string Hash(string value)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}
