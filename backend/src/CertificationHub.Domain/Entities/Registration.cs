using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("Registrations")]
public class Registration : AuditableEntity
{
    [Key]
    public Guid RegistrationId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => RegistrationId;
        set => RegistrationId = value;
    }

    public Guid DriveId { get; set; }

    public Guid UserId { get; set; }

    [MaxLength(150)]
    public string ExamTrack { get; set; } = string.Empty;

    [MaxLength(150)]
    public string PreferredSlot { get; set; } = string.Empty;

    public int PriorAttempts { get; set; }

    public Guid? ApproverId { get; set; }

    public DateTime? ApprovalDate { get; set; }

    [MaxLength(1000)]
    public string ApprovalComments { get; set; } = string.Empty;

    public RegistrationStatus Status { get; set; } = RegistrationStatus.Pending;

    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(DriveId))]
    public CertificationDrive? Drive { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public EligibilityRecord? EligibilityRecord { get; set; }

    public AssessmentResult? AssessmentResult { get; set; }

    public Voucher? VoucherAllocation { get; set; }
}
