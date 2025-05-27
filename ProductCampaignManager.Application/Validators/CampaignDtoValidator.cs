using FluentValidation;
using ProductCampaignManager.Application.DTOs;

namespace ProductCampaignManager.Application.Validators;

public class CampaignDtoValidator : AbstractValidator<CampaignDto>
{
    public CampaignDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Campaign name is required.");

        RuleFor(x => x.Keywords)
            .NotEmpty().WithMessage("At least one keyword is required.")
            .Must(keywords => keywords.All(k => !string.IsNullOrWhiteSpace(k)))
            .WithMessage("Keywords must not contain empty values.");

        RuleFor(x => x.BidAmount)
            .GreaterThanOrEqualTo(0.01m).WithMessage("Bid amount must be at least 0.01.");

        RuleFor(x => x.CampaignFund)
            .GreaterThan(0).WithMessage("Campaign fund must be greater than zero.");

        RuleFor(x => x.Status)
            .Must(status => status == "On" || status == "Off")
            .WithMessage("Status must be 'On' or 'Off'.");

        RuleFor(x => x.Town)
            .NotEmpty().WithMessage("Town is required.");

        RuleFor(x => x.RadiusInKm)
            .GreaterThan(0).WithMessage("Radius must be greater than 0 km.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}