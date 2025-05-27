using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Domain.Entities;

public class Campaign
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public List<string> Keywords { get; private set; } = new();
    public decimal BidAmount { get; private set; }
    public decimal CampaignFund { get; private set; }
    public CampaignStatus Status { get; private set; }
    public string Town { get; private set; } = null!;
    public int RadiusInKm { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;

    private Campaign() { }

    public Campaign(
        string name,
        List<string> keywords,
        decimal bidAmount,
        decimal campaignFund,
        CampaignStatus status,
        string town,
        int radiusInKm,
        Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Keywords = keywords;
        BidAmount = bidAmount;
        CampaignFund = campaignFund;
        Status = status;
        Town = town;
        RadiusInKm = radiusInKm;
        UserId = userId;
    }

    public void TurnOn() => Status = CampaignStatus.On;
    public void TurnOff() => Status = CampaignStatus.Off;
}