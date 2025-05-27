using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProductCampaignManager.Application.DTOs;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Application.Commands.Handlers;

public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand>
{
    private readonly ICampaignRepository _repository;
    private readonly IValidator<CampaignDto> _validator;

    public UpdateCampaignCommandHandler(ICampaignRepository repository, IValidator<CampaignDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        ValidationResult validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => $"{e.PropertyName}: {e.ErrorMessage}");

            throw new ValidationException("CampaignDto validation failed:\n" + string.Join("\n", errors));
        }

        var campaign = await _repository.GetByIdAsync(request.Id);
        if (campaign == null)
            throw new KeyNotFoundException("Campaign not found");

        campaign.Update(dto.Name, dto.Keywords, dto.BidAmount, dto.CampaignFund,
            Enum.Parse<CampaignStatus>(dto.Status), dto.Town, dto.RadiusInKm);

        await _repository.UpdateAsync(campaign);
    }
}