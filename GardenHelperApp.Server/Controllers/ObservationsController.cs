using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObservationsController : ControllerBase
{
    private readonly GardenContext _context;

    public ObservationsController(GardenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ObservationModel>>> GetAll()
    {
        return await _context.Observations.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObservationModel>> Get(int id)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null) return NotFound();
        return obs;
    }

    [HttpGet("plant/{plantId}")]
    public async Task<ActionResult<List<ObservationModel>>> GetByPlant(int plantId)
    {
        return await _context.Observations
            .Where(o => o.PlantId == plantId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ObservationModel model)
    {
        _context.Observations.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ObservationModel model)
    {
        if (id != model.ObservationId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null) return NotFound();

        _context.Observations.Remove(obs);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
