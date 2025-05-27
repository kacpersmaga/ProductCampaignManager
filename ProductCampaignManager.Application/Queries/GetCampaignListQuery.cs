using MediatR;
using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Queries;

public record GetCampaignListQuery(Guid UserId) : IRequest<List<Campaign>>;