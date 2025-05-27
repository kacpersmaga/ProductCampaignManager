using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Infrastructure.Persistence.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.BidAmount)
            .HasPrecision(18, 2);

        builder.Property(c => c.CampaignFund)
            .HasPrecision(18, 2);

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.Town)
            .IsRequired();

        builder.Property(c => c.RadiusInKm)
            .IsRequired();

        builder.Property(c => c.Keywords)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        builder.HasOne(c => c.User)
            .WithMany(u => u.Campaigns)
            .HasForeignKey(c => c.UserId);
    }
}