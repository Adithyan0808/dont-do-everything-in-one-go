using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("CertificationDrives")]
public class CertificationDrive : AuditableEntity
{
    [Key]
    public Guid DriveId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => DriveId;
        set => DriveId = value;
    }

    [Required]
    [MaxLength(50)]
    public string DriveCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string DriveName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string VendorName { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string CertificationName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string SponsorName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal BudgetAllocated { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BudgetConsumed { get; set; }

    public bool ManagerApprovalRequired { get; set; }

    public int AttemptLimit { get; set; }

    public int TenureRequiredDays { get; set; }

    public DateTime RegistrationStartDate { get; set; }

    public DateTime RegistrationEndDate { get; set; }

    public DateTime ExamWindowStartDate { get; set; }

    public DateTime ExamWindowEndDate { get; set; }

    public int TargetCount { get; set; }

    public int CurrentRegistrationCount { get; set; }

    public int CurrentCertifiedCount { get; set; }

    [MaxLength(1000)]
    public string PolicyUrl { get; set; } = string.Empty;

    public DriveStatus Status { get; set; } = DriveStatus.Draft;

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();

    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
