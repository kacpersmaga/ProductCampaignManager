using MediatR;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Queries.Handlers;

public class GetCampaignListQueryHandler : IRequestHandler<GetCampaignListQuery, List<Campaign>>
{
    private readonly ICampaignRepository _repository;

    public GetCampaignListQueryHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Campaign>> Handle(GetCampaignListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByUserIdAsync(request.UserId);
    }
}