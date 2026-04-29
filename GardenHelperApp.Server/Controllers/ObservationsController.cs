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
        // Order results by CreatedAt only (ignore UpdatedAt)
        return await _context.Observations
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
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
    public async Task<ActionResult<int>> Create(ObservationModel model)
    {
        model.CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
        model.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);



        if (model.UserId <= 0)
            return BadRequest("UserId is required.");

        _context.Observations.Add(model);
        await _context.SaveChangesAsync();

        return model.ObservationId;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ObservationModel model)
    {
        if (id != model.ObservationId)
            return BadRequest();

        // Keep original CreatedAt (client may send it) but normalize UpdatedAt to UTC now
        model.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

        try
        {
            _context.Observations.Update(model);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

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

    [HttpPut("{id}/active")]
    public async Task<IActionResult> UpdateActiveStatus(int id, [FromBody] bool isActive)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null)
            return NotFound();

        obs.ActiveObservation = isActive;
        obs.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<ObservationModel>>> GetByUser(int userId)
    {
        // Ensure server returns observations ordered by CreatedAt only
        return await _context.Observations
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }


}
