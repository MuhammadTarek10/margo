using AutoMapper;
using Domain.Entities;
using Application.Features.Auth.DTOs;

namespace Application.Features.Auth.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}