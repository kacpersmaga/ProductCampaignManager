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
    private readonly IUserRepository _userRepository;

    public UpdateCampaignCommandHandler(
        ICampaignRepository repository,
        IValidator<CampaignDto> validator,
        IUserRepository userRepository)
    {
        _repository = repository;
        _validator = validator;
        _userRepository = userRepository;
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

        if (!Enum.TryParse<CampaignStatus>(dto.Status, true, out var status))
        {
            throw new ArgumentException($"Invalid campaign status: {dto.Status}");
        }

        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new ArgumentException("User not found");

        var fundDifference = dto.CampaignFund - campaign.CampaignFund;
        if (fundDifference > 0)
        {
            if (!user.CanAfford(fundDifference))
                throw new InvalidOperationException("Insufficient funds to increase campaign fund.");

            user.Deduct(fundDifference);
            await _userRepository.UpdateAsync(user);
        }
        
        campaign.Update(
            dto.Name,
            dto.Keywords,
            dto.BidAmount,
            dto.CampaignFund,
            status,
            dto.Town,
            dto.RadiusInKm
        );

        await _repository.UpdateAsync(campaign);
    }
}
