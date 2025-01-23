
using AutoMapper;

using Domain.Entities;

namespace Application.Features.DTOs;

public class ChatProfile : Profile
{

    public ChatProfile()
    {
        CreateMap<Chat, ChatDto>()
            .ForMember(c => c.Messages, opt => opt.Ignore())
            .ForMember(c => c.AgentName, opt => opt.MapFrom(c => c.Agent == null ? "No Agent Present" : c.Agent.Name))
            .ForMember(c => c.CustomerName, opt => opt.MapFrom(c => c.User == null ? "No Customer Present" : c.User.Name))
            .ForMember(c => c.LastMessage, opt => opt.MapFrom(c => c.Messages.LastOrDefault()));

        CreateMap<ChatDto, Chat>();
        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, Message>();
    }

}