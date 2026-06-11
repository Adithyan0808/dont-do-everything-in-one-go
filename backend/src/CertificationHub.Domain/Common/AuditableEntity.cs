using System.ComponentModel.DataAnnotations;

namespace CertificationHub.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    [MaxLength(256)]
    public string DeletedBy { get; set; } = string.Empty;
}
