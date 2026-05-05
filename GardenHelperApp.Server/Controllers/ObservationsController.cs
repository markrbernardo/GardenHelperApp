using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObservationsController : ControllerBase
{
    private readonly GardenContext _context;

    public ObservationsController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET ALL (rarely used)
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<ObservationModel>>> GetAllAsync()
    {
        return await _context.Observations
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ObservationModel>> GetAsync(int id)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null)
            return NotFound();

        return obs;
    }

    // ---------------------------------------------------------
    // GET BY PLANT
    // ---------------------------------------------------------
    [HttpGet("plant/{plantId:int}")]
    public async Task<ActionResult<List<ObservationModel>>> GetByPlantAsync(int plantId)
    {
        return await _context.Observations
            .Where(o => o.PlantId == plantId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY USER
    // ---------------------------------------------------------
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<ObservationModel>>> GetByUserAsync(int userId)
    {
        return await _context.Observations
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY DATE RANGE (ALL OBSERVATIONS)
    // ---------------------------------------------------------
    [HttpGet("range")]
    public async Task<ActionResult<List<ObservationModel>>> GetByDateRangeAsync(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        var results = await _context.Observations
            .Where(o => o.CreatedAt >= start && o.CreatedAt <= end)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return results;
    }

    // ---------------------------------------------------------
    // GET BY PLANT + DATE RANGE
    // ---------------------------------------------------------
    [HttpGet("plant/{plantId:int}/range")]
    public async Task<ActionResult<List<ObservationModel>>> GetByPlantAndDateRangeAsync(
        int plantId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        var results = await _context.Observations
            .Where(o => o.PlantId == plantId &&
                        o.CreatedAt >= start &&
                        o.CreatedAt <= end)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return results;
    }

    // ---------------------------------------------------------
    // GET BY USER + DATE RANGE
    // ---------------------------------------------------------
    [HttpGet("user/{userId:int}/range")]
    public async Task<ActionResult<List<ObservationModel>>> GetByUserAndDateRangeAsync(
        int userId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        var results = await _context.Observations
            .Where(o => o.UserId == userId &&
                        o.CreatedAt >= start &&
                        o.CreatedAt <= end)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return results;
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<int>> CreateAsync(ObservationModel model)
    {
        if (model.UserId <= 0)
            return BadRequest("UserId is required.");

        // Server controls timestamps
        model.CreatedAt = DateTime.Now;
        model.UpdatedAt = DateTime.Now;

        _context.Observations.Add(model);
        await _context.SaveChangesAsync();

        return model.ObservationId;
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, ObservationModel model)
    {
        if (id != model.ObservationId)
            return BadRequest("Observation ID mismatch.");

        // Preserve CreatedAt, update UpdatedAt
        model.UpdatedAt = DateTime.Now;

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // UPDATE ACTIVE STATUS
    // ---------------------------------------------------------
    [HttpPut("{id:int}/active")]
    public async Task<IActionResult> UpdateActiveStatusAsync(int id, [FromBody] bool isActive)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null)
            return NotFound();

        obs.ActiveObservation = isActive;
        obs.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var obs = await _context.Observations.FindAsync(id);
        if (obs == null)
            return NotFound();

        _context.Observations.Remove(obs);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
