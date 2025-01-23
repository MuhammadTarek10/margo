
namespace Application.Features.DTOs;

public class MessageDto
{
    public Guid ChatId { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecieverId { get; set; }
    public required string Content { get; set; }
}