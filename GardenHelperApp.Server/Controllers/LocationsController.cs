using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly GardenContext _context;

    public LocationsController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET ALL LOCATIONS
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<LocationModel>>> GetAllAsync()
    {
        var locations = await _context.Locations
            .OrderBy(l => l.Name)
            .ToListAsync();

        return locations;
    }

    // ---------------------------------------------------------
    // GET SINGLE LOCATION
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LocationModel>> GetAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
            return NotFound();

        return location;
    }

    // ---------------------------------------------------------
    // GET LOCATIONS BY GARDEN
    // ---------------------------------------------------------
    [HttpGet("garden/{gardenId:int}")]
    public async Task<ActionResult<List<LocationModel>>> GetByGardenAsync(int gardenId)
    {
        var locations = await _context.Locations
            .Where(l => l.GardenId == gardenId)
            .OrderBy(l => l.Name)
            .ToListAsync();

        return locations;
    }

    // ---------------------------------------------------------
    // CREATE LOCATION
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync(LocationModel model)
    {
        _context.Locations.Add(model);
        await _context.SaveChangesAsync();

        return Ok(model.LocationId);
    }

    // ---------------------------------------------------------
    // UPDATE LOCATION
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, LocationModel model)
    {
        if (id != model.LocationId)
            return BadRequest("Location ID mismatch.");

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // DELETE LOCATION
    // ---------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
            return NotFound();

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
