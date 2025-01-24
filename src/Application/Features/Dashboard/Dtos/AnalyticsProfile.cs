using AutoMapper;

using Domain.Entities;

namespace Application.Features.DTOs;

public class AnalyticsProfile : Profile
{

    public AnalyticsProfile()
    {
        CreateMap<Analytics, AnalyticsDto>();
        CreateMap<AnalyticsDto, Analytics>();
    }
}