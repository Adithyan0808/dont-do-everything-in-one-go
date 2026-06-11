using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CertificationHub.Domain.Common;
using CertificationHub.Domain.Enums;

namespace CertificationHub.Domain.Entities;

[Table("Users")]
public class User : AuditableEntity
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();

    [NotMapped]
    public override Guid Id
    {
        get => UserId;
        set => UserId = value;
    }

    [Required]
    [MaxLength(50)]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Department { get; set; } = string.Empty;

    [MaxLength(150)]
    public string BusinessUnit { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(256)]
    public string ManagerEmail { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.Employee;

    [Required]
    [MaxLength(512)]
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime LastLoginDate { get; set; }

    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    public ICollection<EmployeeCertification> EmployeeCertifications { get; set; } = new List<EmployeeCertification>();

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
