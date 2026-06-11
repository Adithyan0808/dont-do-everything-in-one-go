using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("Notifications")]
public class Notification : AuditableEntity
{
    [Key]
    public Guid NotificationId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => NotificationId;
        set => NotificationId = value;
    }

    public Guid UserId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; } = NotificationType.Information;

    public bool IsRead { get; set; }

    public SLAStatus SLAStatus { get; set; } = SLAStatus.NotApplicable;

    public DateTime? ReadDate { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
