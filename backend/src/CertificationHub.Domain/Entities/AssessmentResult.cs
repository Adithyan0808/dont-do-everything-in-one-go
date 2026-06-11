using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("AssessmentResults")]
public class AssessmentResult : AuditableEntity
{
    [Key]
    public Guid ResultId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => ResultId;
        set => ResultId = value;
    }

    public Guid RegistrationId { get; set; }

    public int Score { get; set; }

    public int MaxScore { get; set; }

    public AssessmentOutcome Outcome { get; set; } = AssessmentOutcome.Pending;

    [MaxLength(1000)]
    public string EvidenceUrl { get; set; } = string.Empty;

    [MaxLength(256)]
    public string SubmittedBy { get; set; } = string.Empty;

    public DateTime SubmissionDate { get; set; }

    [ForeignKey(nameof(RegistrationId))]
    public Registration? Registration { get; set; }
}
