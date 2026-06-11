using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;

namespace CertificationHub.Domain.Entities;

[Table("AuditLogs")]
public class AuditLog : BaseEntity
{
    [Key]
    public Guid AuditLogId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => AuditLogId;
        set => AuditLogId = value;
    }

    public Guid? DriveId { get; set; }

    [Required]
    [MaxLength(200)]
    public string EntityName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string EntityId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    public Guid ActorId { get; set; }

    [MaxLength(200)]
    public string ActorName { get; set; } = string.Empty;

    public string BeforeSnapshotJson { get; set; } = string.Empty;

    public string AfterSnapshotJson { get; set; } = string.Empty;

    [MaxLength(64)]
    public string IPAddress { get; set; } = string.Empty;

    [MaxLength(512)]
    public string UserAgent { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(DriveId))]
    public CertificationDrive? Drive { get; set; }
}
