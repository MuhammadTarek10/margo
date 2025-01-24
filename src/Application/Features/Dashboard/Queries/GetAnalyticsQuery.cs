
using Application.Features.DTOs;

using AutoMapper;

using Domain.Interfaces;

using MediatR;

namespace Application.Features.Queries;

public class GetAnalyticsQuery : IRequest<AnalyticsDto> { }


public class GetAnalyticsQueryHandler(
    IMapper mapper,
    IDashboardRepository repository) : IRequestHandler<GetAnalyticsQuery, AnalyticsDto>
{

    public async Task<AnalyticsDto> Handle(GetAnalyticsQuery request, CancellationToken cancellationToken)
    {
        var analytics = await repository.GetCurrentAnalytics();
        return mapper.Map<AnalyticsDto>(analytics);
    }
}