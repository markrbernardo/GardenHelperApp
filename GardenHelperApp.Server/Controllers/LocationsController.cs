using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly GardenContext _context;

    public LocationsController(GardenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<LocationModel>>> GetAll()
    {
        return await _context.Locations.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationModel>> Get(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();
        return location;
    }

    [HttpGet("garden/{gardenId}")]
    public async Task<ActionResult<List<LocationModel>>> GetByGarden(int gardenId)
    {
        return await _context.Locations
            .Where(l => l.GardenId == gardenId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(LocationModel model)
    {
        _context.Locations.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LocationModel model)
    {
        if (id != model.LocationId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(int id, LocationModel model)
    {
        if (id != model.LocationId)
            return BadRequest();

        _context.Locations.Update(model);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
