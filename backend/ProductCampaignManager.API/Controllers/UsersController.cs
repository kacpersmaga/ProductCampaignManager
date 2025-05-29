using Microsoft.AspNetCore.Mvc;
using ProductCampaignManager.Infrastructure.Persistence;

namespace ProductCampaignManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:guid}/balance")]
    public async Task<IActionResult> GetBalance(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound("User not found");

        return Ok(new { Balance = user.EmeraldBalance });
    }
}