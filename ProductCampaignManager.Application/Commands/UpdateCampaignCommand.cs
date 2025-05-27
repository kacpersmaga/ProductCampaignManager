using MediatR;
using ProductCampaignManager.Application.DTOs;

namespace ProductCampaignManager.Application.Commands;

public record UpdateCampaignCommand(Guid Id, CampaignDto Dto) : IRequest;