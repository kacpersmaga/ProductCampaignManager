namespace ProductCampaignManager.Application.DTOs;

public class CampaignDto
{
    public string Name { get; set; } = default!;
    public List<string> Keywords { get; set; } = new();
    public decimal BidAmount { get; set; }
    public decimal CampaignFund { get; set; }
    public string Status { get; set; } = default!;
    public string Town { get; set; } = default!;
    public int RadiusInKm { get; set; }
    public Guid UserId { get; set; }
}