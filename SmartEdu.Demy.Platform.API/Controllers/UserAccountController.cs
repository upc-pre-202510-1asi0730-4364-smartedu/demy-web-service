using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace SmartEdu.Demy.Platform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserAccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.UserAccounts.ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _context.UserAccounts.FindAsync(id);
        return user == null ? NotFound() : Ok(user);
    }
    
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("pong desde Swagger");
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserAccount user)
    {
        _context.UserAccounts.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
    }
}