using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly GardenContext _context;
    private readonly ILogger<UsersController> _logger;


    public UsersController(GardenContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInformation("DB Path: {Path}", Path.GetFullPath("Data/GardenHelper.db"));
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _logger.LogInformation("DB Path: {Path}", Path.GetFullPath("Data/GardenHelper.db"));
        return user;
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
    }

    // PUT: api/users/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserModel user)
    {
        if (id != user.UserId) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //SET: Default Garden
    [HttpPut("{id}/default-garden/{gardenId}")]
    public async Task<IActionResult> SetDefaultGarden(int id, int gardenId)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.DefaultGardenId = gardenId;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    //GET: Default Garden
    [HttpGet("{id}/default-garden")]
    public async Task<ActionResult<int?>> GetDefaultGarden(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        return user.DefaultGardenId;
    }



}
