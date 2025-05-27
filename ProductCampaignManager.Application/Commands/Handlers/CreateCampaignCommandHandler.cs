using MediatR;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Entities;
using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Application.Commands.Handlers;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Guid>
{
    private readonly ICampaignRepository _repository;

    public CreateCampaignCommandHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var campaign = new Campaign(
            name: dto.Name,
            keywords: dto.Keywords,
            bidAmount: dto.BidAmount,
            campaignFund: dto.CampaignFund,
            status: Enum.Parse<CampaignStatus>(dto.Status),
            town: dto.Town,
            radiusInKm: dto.RadiusInKm,
            userId: dto.UserId
        );

        await _repository.AddAsync(campaign);
        return campaign.Id;
    }
}