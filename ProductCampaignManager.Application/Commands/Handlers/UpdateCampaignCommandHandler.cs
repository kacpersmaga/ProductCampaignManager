using MediatR;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Application.Commands.Handlers;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand>
{
    private readonly ICampaignRepository _repository;

    public UpdateCampaignCommandHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = await _repository.GetByIdAsync(request.Id);
        if (campaign == null)
            throw new KeyNotFoundException("Campaign not found");

        var dto = request.Dto;
        
        campaign.Update(dto.Name, dto.Keywords, dto.BidAmount, dto.CampaignFund,
            Enum.Parse<CampaignStatus>(dto.Status), dto.Town, dto.RadiusInKm);

        await _repository.UpdateAsync(campaign);
    }
}