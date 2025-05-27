using MediatR;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Queries.Handlers;


public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, Campaign>
{
    private readonly ICampaignRepository _repository;

    public GetCampaignByIdQueryHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task<Campaign> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
    {
        var campaign = await _repository.GetByIdAsync(request.Id);
        if (campaign is null)
            throw new KeyNotFoundException("Campaign not found");
        return campaign;
    }
}