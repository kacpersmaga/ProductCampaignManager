using ProductCampaignManager.Domain.Entities;

namespace ProductCampaignManager.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task UpdateAsync(User user);
}