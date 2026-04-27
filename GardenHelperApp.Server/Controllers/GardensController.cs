using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;

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

    // GET: api/gardens/user/2
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<GardenModel>>> GetGardensByUser(int userId)
    {
        return await _context.Gardens
            .Where(g => g.UserId == userId)
            .ToListAsync();
    }

    // GET: api/gardens/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GardenModel>> GetGarden(int id)
    {
        var garden = await _context.Gardens.FindAsync(id);
        if (garden == null) return NotFound();
        return garden;
    }

    // GET: api/gardens
    [HttpGet]
    public async Task<ActionResult<List<GardenModel>>> GetAllGardens()
    {
        return await _context.Gardens.ToListAsync();
    }

    // POST: api/gardens
    [HttpPost]
    public async Task<ActionResult<GardenModel>> CreateGarden(GardenModel garden)
    {
        // Validate FK
        var userExists = await _context.Users.AnyAsync(u => u.UserId == garden.UserId);
        if (!userExists)
            return BadRequest($"User with ID {garden.UserId} does not exist.");

        // Create the garden
        _context.Gardens.Add(garden);
        await _context.SaveChangesAsync();

        // Automatically create the default "Undecided" location
        var undecided = new LocationModel
        {
            GardenId = garden.GardenId,
            Name = "Undecided",
            Lighting = "N/A",
            IsOutside = null
        };

        _context.Locations.Add(undecided);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGarden), new { id = garden.GardenId }, garden);
    }

    // PUT: api/gardens/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGarden(int id, GardenModel garden)
    {
        if (id != garden.GardenId)
            return BadRequest();

        // Validate FK
        var userExists = await _context.Users.AnyAsync(u => u.UserId == garden.UserId);
        if (!userExists)
            return BadRequest($"User with ID {garden.UserId} does not exist.");

        _context.Entry(garden).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/gardens/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGarden(int id)
    {
        var garden = await _context.Gardens.FindAsync(id);
        if (garden == null) return NotFound();

        // Cascade delete will remove Locations + Plants automatically
        _context.Gardens.Remove(garden);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{userId}/default-garden/{gardenId}")]
    public async Task<IActionResult> SetDefaultGarden(int userId, int gardenId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound();

        user.DefaultGardenId = gardenId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{userId}/default-garden")]
    public async Task<ActionResult<int?>> GetDefaultGarden(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound();

        return user.DefaultGardenId;
    }

    [HttpGet("locations")]
    public async Task<IEnumerable<LocationModel>> GetAllLocations()
    {
        return await _context.Locations.ToListAsync();
    }

}
