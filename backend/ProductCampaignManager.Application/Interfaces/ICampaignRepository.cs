using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Interfaces;


public interface ICampaignRepository
{
    Task<Campaign?> GetByIdAsync(Guid id);
    Task<List<Campaign>> GetByUserIdAsync(Guid userId);
    Task AddAsync(Campaign campaign);
    Task UpdateAsync(Campaign campaign);
    Task DeleteAsync(Campaign campaign);
    Task<List<Campaign>> GetAllAsync();
}