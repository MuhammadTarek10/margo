
namespace Application.Features.DTOs;

public class ChatDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid AgentId { get; set; }

    public required string CustomerName { get; set; }
    public required string AgentName { get; set; }

    public List<MessageDto> Messages { get; set; } = new List<MessageDto>();

    public required MessageDto LastMessage { get; set; }
}