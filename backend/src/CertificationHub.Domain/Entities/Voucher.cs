using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("Vouchers")]
public class Voucher : AuditableEntity
{
    [Key]
    public Guid VoucherId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => VoucherId;
        set => VoucherId = value;
    }

    public Guid DriveId { get; set; }

    [MaxLength(150)]
    public string Vendor { get; set; } = string.Empty;

    [Required]
    public string EncryptedCode { get; set; } = string.Empty;

    [MaxLength(100)]
    public string MaskedCode { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    public VoucherStatus Status { get; set; } = VoucherStatus.Available;

    public Guid? AssignedToUserId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime? RedeemedDate { get; set; }

    [MaxLength(1000)]
    public string TokenizedLink { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    [ForeignKey(nameof(DriveId))]
    public CertificationDrive? Drive { get; set; }

    [ForeignKey(nameof(AssignedToUserId))]
    public User? AssignedToUser { get; set; }
}
