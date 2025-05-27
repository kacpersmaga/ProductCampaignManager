using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCampaignManager.Application.DTOs;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Entities;
using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Application.Commands.Handlers;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Guid>
{
    private readonly ICampaignRepository _repository;
    private readonly IValidator<CampaignDto> _validator;

    public CreateCampaignCommandHandler(ICampaignRepository repository, IValidator<CampaignDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        
        ValidationResult validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

            throw new ValidationException("CampaignDto validation failed:\n" + string.Join("\n", errors));
        }

        if (!Enum.TryParse<CampaignStatus>(dto.Status, true, out var status))
        {
            throw new ArgumentException($"Invalid campaign status: {dto.Status}");
        }

        var campaign = new Campaign(
            name: dto.Name,
            keywords: dto.Keywords,
            bidAmount: dto.BidAmount,
            campaignFund: dto.CampaignFund,
            status: status,
            town: dto.Town,
            radiusInKm: dto.RadiusInKm,
            userId: dto.UserId
        );

        await _repository.AddAsync(campaign);
        return campaign.Id;
    }
}