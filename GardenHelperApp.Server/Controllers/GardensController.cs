using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GardensController : ControllerBase
{
    private readonly GardenContext _context;

    public GardensController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET ALL GARDENS FOR A USER
    // ---------------------------------------------------------
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<GardenModel>>> GetByUserAsync(int userId)
    {
        var gardens = await _context.Gardens
            .Where(g => g.UserId == userId)
            .ToListAsync();

        return gardens;
    }

    // ---------------------------------------------------------
    // GET SINGLE GARDEN
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GardenModel>> GetAsync(int id)
    {
        var garden = await _context.Gardens.FindAsync(id);
        if (garden == null)
            return NotFound();

        return garden;
    }

    // ---------------------------------------------------------
    // CREATE GARDEN
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<GardenModel>> CreateAsync(GardenModel model)
    {
        // Validate FK
        var userExists = await _context.Users.AnyAsync(u => u.UserId == model.UserId);
        if (!userExists)
            return BadRequest($"User with ID {model.UserId} does not exist.");

        _context.Gardens.Add(model);
        await _context.SaveChangesAsync();

        // Create default "Undecided" location
        var undecided = new LocationModel
        {
            GardenId = model.GardenId,
            Name = "Undecided",
            Lighting = LocationLighting.Unknown,
            IsOutside = null
        };

        _context.Locations.Add(undecided);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsync), new { id = model.GardenId }, model);
    }

    // ---------------------------------------------------------
    // UPDATE GARDEN
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, GardenModel model)
    {
        if (id != model.GardenId)
            return BadRequest("Garden ID mismatch.");

        var userExists = await _context.Users.AnyAsync(u => u.UserId == model.UserId);
        if (!userExists)
            return BadRequest($"User with ID {model.UserId} does not exist.");

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // DELETE GARDEN
    // ---------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var garden = await _context.Gardens.FindAsync(id);
        if (garden == null)
            return NotFound();

        _context.Gardens.Remove(garden);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // DEFAULT GARDEN
    // ---------------------------------------------------------
    [HttpPut("{userId:int}/default-garden/{gardenId:int}")]
    public async Task<IActionResult> SetDefaultGardenAsync(int userId, int gardenId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound();

        user.DefaultGardenId = gardenId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{userId:int}/default-garden")]
    public async Task<ActionResult<int?>> GetDefaultGardenAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound();

        return user.DefaultGardenId;
    }
}
