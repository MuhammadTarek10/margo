using MediatR;

using Application.Features.DTOs;
using Application.Features.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Commands;

namespace Api.Controllers;

[ApiController]
[Authorize]
[Route("api/chat")]
public class ChatController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMyChats()
    {
        List<ChatDto> dtos = await mediator.Send(new GetChatsQuery());
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat()
    {
        ChatDto dto = await mediator.Send(new CreateChatCommand());
        return Ok(dto);
    }
}