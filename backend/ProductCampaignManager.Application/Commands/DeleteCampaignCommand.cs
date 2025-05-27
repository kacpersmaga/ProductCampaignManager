using MediatR;

namespace ProductCampaignManager.Application.Commands;

public record DeleteCampaignCommand(Guid Id) : IRequest;