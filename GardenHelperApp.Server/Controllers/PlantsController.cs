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

    // GET: api/plants
    [HttpGet]
    public async Task<ActionResult<List<PlantModel>>> GetAll()
    {
        return await _context.Plants.ToListAsync();
    }

    // GET: api/plants/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PlantModel>> Get(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null) return NotFound();
        return plant;
    }

    // GET: api/plants/garden/3
    [HttpGet("garden/{gardenId}")]
    public async Task<ActionResult<List<PlantModel>>> GetPlantsByGarden(int gardenId)
    {
        var plants = await _context.Plants
            .Where(p => p.GardenId == gardenId)
            .ToListAsync();

        return Ok(plants);
    }

    // GET: api/plants/location/7
    [HttpGet("location/{locationId}")]
    public async Task<ActionResult<List<PlantModel>>> GetPlantsByLocation(int locationId)
    {
        var plants = await _context.Plants
            .Where(p => p.LocationId == locationId)
            .ToListAsync();

        return Ok(plants);
    }

    // GET: api/plants/info/12
    [HttpGet("info/{plantInfoId}")]
    public async Task<ActionResult<List<PlantModel>>> GetByPlantInfo(int plantInfoId)
    {
        return await _context.Plants
            .Where(p => p.PlantInformationId == plantInfoId)
            .ToListAsync();
    }

    // POST: api/plants
    [HttpPost]
    public async Task<IActionResult> Create(PlantModel model)
    {
        _context.Plants.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    // PUT: api/plants/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PlantModel model)
    {
        if (id != model.PlantId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/plants/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null) return NotFound();

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();

        return NoContent();
    }



    [HttpPost("create")]
    public async Task<ActionResult<PlantModel>> CreatePlant(PlantModel plant)
    {
        // Validate Garden exists
        var gardenExists = await _context.Gardens.AnyAsync(g => g.GardenId == plant.GardenId);
        if (!gardenExists)
            return BadRequest($"Garden with ID {plant.GardenId} does not exist.");

        // Find the Undecided location for this garden
        var defaultLocation = await _context.Locations
            .FirstOrDefaultAsync(l => l.GardenId == plant.GardenId && l.Name == "Undecided");

        if (defaultLocation == null)
            return BadRequest("No default 'Undecided' location exists for this garden.");


        _context.Plants.Add(plant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = plant.PlantId }, plant);
    }




}
