using MediatR;
using ProductCampaignManager.Application.Interfaces;

namespace ProductCampaignManager.Application.Commands.Handlers;

public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand>
{
    private readonly ICampaignRepository _repository;

    public DeleteCampaignCommandHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = await _repository.GetByIdAsync(request.Id);
        if (campaign == null)
            throw new KeyNotFoundException("Campaign not found");

        await _repository.DeleteAsync(campaign);
    }
}