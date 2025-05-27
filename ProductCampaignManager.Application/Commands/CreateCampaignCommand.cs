using MediatR;
using ProductCampaignManager.Application.DTOs;

namespace ProductCampaignManager.Application.Commands;

public record CreateCampaignCommand(CampaignDto Dto) : IRequest<Guid>;