using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


public class ChatMessage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey(nameof(Agent))]
    public Guid AgentId { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Message { get; set; }

    [Required]
    public bool IsFromUser { get; set; } // True if the message is from the user, false if from the agent

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public required User User { get; set; }
    public required User Agent { get; set; }
}
