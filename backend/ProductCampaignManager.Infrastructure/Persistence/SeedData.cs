using ProductCampaignManager.Domain.Entities;
using ProductCampaignManager.Domain.Enums;

namespace ProductCampaignManager.Infrastructure.Persistence;


public static class SeedData
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            var userId = new Guid("00000000-0000-0000-0000-000000000001");
            var user = new User(userId, "Test User", emeraldBalance: 1000m);
            context.Users.Add(user);

            var campaign = new Campaign(
                name: "Launch Campaign",
                keywords: new List<string> { "launch", "product" },
                bidAmount: 1.50m,
                campaignFund: 500m,
                status: CampaignStatus.On,
                town: "Kraków",
                radiusInKm: 10,
                userId: userId
            );

            context.Campaigns.Add(campaign);
            context.SaveChanges();
        }
    }
}