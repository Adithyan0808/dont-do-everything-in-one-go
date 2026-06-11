using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;

namespace CertificationHub.Domain.Entities;

[Table("EmployeeCertifications")]
public class EmployeeCertification : AuditableEntity
{
    [Key]
    public Guid CertificationId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => CertificationId;
        set => CertificationId = value;
    }

    public Guid UserId { get; set; }

    [Required]
    [MaxLength(250)]
    public string CertificationName { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string VendorName { get; set; } = string.Empty;

    [MaxLength(150)]
    public string CredentialId { get; set; } = string.Empty;

    public DateTime IssuedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsExpired { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
