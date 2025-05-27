using MediatR;
using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Queries;


public record GetCampaignByIdQuery(Guid Id) : IRequest<Campaign>;