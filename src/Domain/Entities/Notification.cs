using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Notification
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Subject { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Message { get; set; }

    [Required]
    public Guid RecipientId { get; set; }

    [Required]
    public NotificationStatus Status { get; set; }

    [Required]
    public bool IsRead { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime SentAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }


    public enum NotificationStatus
    {
        Pending,    // Notification is created but not yet sent
        Sent,       // Notification has been sent
        Failed      // Notification failed to send
    }
}