using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;

namespace CertificationHub.Domain.Entities;

[Table("EligibilityRecords")]
public class EligibilityRecord : AuditableEntity
{
    [Key]
    public Guid EligibilityId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => EligibilityId;
        set => EligibilityId = value;
    }

    public Guid RegistrationId { get; set; }

    [Required]
    public string CriteriaJson { get; set; } = "{}";

    public bool IsEligible { get; set; }

    [MaxLength(1000)]
    public string DecisionReason { get; set; } = string.Empty;

    public Guid ApproverId { get; set; }

    public DateTime DecisionDate { get; set; }

    [MaxLength(1000)]
    public string Remarks { get; set; } = string.Empty;

    [ForeignKey(nameof(RegistrationId))]
    public Registration? Registration { get; set; }
}
