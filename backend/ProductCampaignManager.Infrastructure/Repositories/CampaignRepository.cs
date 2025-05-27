using Microsoft.EntityFrameworkCore;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Domain.Entities;
using ProductCampaignManager.Infrastructure.Persistence;

namespace ProductCampaignManager.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly AppDbContext _context;

    public CampaignRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Campaign?> GetByIdAsync(Guid id) =>
        await _context.Campaigns.FindAsync(id);

    public async Task<List<Campaign>> GetByUserIdAsync(Guid userId) =>
        await _context.Campaigns
            .Where(c => c.UserId == userId)
            .ToListAsync();

    public async Task AddAsync(Campaign campaign)
    {
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Campaign campaign)
    {
        _context.Campaigns.Update(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Campaign campaign)
    {
        _context.Campaigns.Remove(campaign);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Campaign>> GetAllAsync()
    {
        return await _context.Campaigns.ToListAsync();
    }
}