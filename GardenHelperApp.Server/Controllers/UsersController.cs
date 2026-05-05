using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;

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
    }

    // ---------------------------------------------------------
    // GET ALL USERS
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetAllAsync()
    {
        return await _context.Users
            .OrderBy(u => u.UserId)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET SINGLE USER
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserModel>> GetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return user;
    }

    // ---------------------------------------------------------
    // CREATE USER
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<UserModel>> CreateAsync(UserModel model)
    {
        _context.Users.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsync), new { id = model.UserId }, model);
    }

    // ---------------------------------------------------------
    // UPDATE USER
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, UserModel model)
    {
        if (id != model.UserId)
            return BadRequest("User ID mismatch.");

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // DELETE USER
    // ---------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // SET DEFAULT GARDEN
    // ---------------------------------------------------------
    [HttpPut("{id:int}/default-garden/{gardenId:int}")]
    public async Task<IActionResult> SetDefaultGardenAsync(int id, int gardenId)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        user.DefaultGardenId = gardenId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // GET DEFAULT GARDEN
    // ---------------------------------------------------------
    [HttpGet("{id:int}/default-garden")]
    public async Task<ActionResult<int?>> GetDefaultGardenAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return user.DefaultGardenId;
    }
}
