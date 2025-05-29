using ProductCampaignManager.Domain.Entities;
using ProductCampaignManager.Infrastructure.Persistence;
using ProductCampaignManager.Application.Interfaces;

namespace ProductCampaignManager.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.FindAsync(id);

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
