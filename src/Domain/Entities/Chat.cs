using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Chat
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid AgentId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [ForeignKey(nameof(AgentId))]
    public User? Agent { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}


public class Message
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ChatId { get; set; }

    [Required]
    public Guid SenderId { get; set; }

    [Required]
    public required string Content { get; set; }

    [Required]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    [ForeignKey(nameof(ChatId))]
    public Chat? Chat { get; set; }
}