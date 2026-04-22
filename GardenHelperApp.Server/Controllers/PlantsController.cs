using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlantsController : ControllerBase
{
    private readonly GardenContext _context;

    public PlantsController(GardenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlantModel>>> GetAll()
    {
        return await _context.Plants.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlantModel>> Get(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null) return NotFound();
        return plant;
    }

    [HttpGet("location/{locationId}")]
    public async Task<ActionResult<List<PlantModel>>> GetByLocation(int locationId)
    {
        return await _context.Plants
            .Where(p => p.LocationId == locationId)
            .ToListAsync();
    }

    [HttpGet("info/{plantInfoId}")]
    public async Task<ActionResult<List<PlantModel>>> GetByPlantInfo(int plantInfoId)
    {
        return await _context.Plants
            .Where(p => p.PlantInformationId == plantInfoId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlantModel model)
    {
        _context.Plants.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PlantModel model)
    {
        if (id != model.PlantId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null) return NotFound();

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
