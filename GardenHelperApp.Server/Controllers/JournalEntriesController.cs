using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Server.Data;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JournalEntriesController : ControllerBase
{
    private readonly GardenContext _context;

    public JournalEntriesController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET ALL (rarely used)
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<JournalEntryModel>>> GetAllAsync()
    {
        return await _context.JournalEntries
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<JournalEntryModel>> GetAsync(int id)
    {
        var entry = await _context.JournalEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        return entry;
    }

    // ---------------------------------------------------------
    // GET BY USER
    // ---------------------------------------------------------
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByUserAsync(int userId)
    {
        return await _context.JournalEntries
            .Where(j => j.UserId == userId)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY GARDEN
    // ---------------------------------------------------------
    [HttpGet("garden/{gardenId:int}")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByGardenAsync(int gardenId)
    {
        return await _context.JournalEntries
            .Where(j => j.GardenId == gardenId)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY DATE RANGE (ALL JOURNAL ENTRIES)
    // ---------------------------------------------------------
    [HttpGet("range")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByDateRangeAsync(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        return await _context.JournalEntries
            .Where(j => j.CreatedAt >= start && j.CreatedAt <= end)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY USER + DATE RANGE
    // ---------------------------------------------------------
    [HttpGet("user/{userId:int}/range")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByUserAndDateRangeAsync(
        int userId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        return await _context.JournalEntries
            .Where(j => j.UserId == userId &&
                        j.CreatedAt >= start &&
                        j.CreatedAt <= end)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET BY GARDEN + DATE RANGE
    // ---------------------------------------------------------
    [HttpGet("garden/{gardenId:int}/range")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByGardenAndDateRangeAsync(
        int gardenId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        if (end < start)
            return BadRequest("End date must be greater than or equal to start date.");

        return await _context.JournalEntries
            .Where(j => j.GardenId == gardenId &&
                        j.CreatedAt >= start &&
                        j.CreatedAt <= end)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<JournalEntryModel>> CreateAsync(JournalEntryModel model)
    {
        // Server controls timestamps
        model.CreatedAt = DateTime.UtcNow;

        _context.JournalEntries.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsync), new { id = model.JournalEntryId }, model);
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, JournalEntryModel model)
    {
        if (id != model.JournalEntryId)
            return BadRequest("JournalEntry ID mismatch.");

        // Preserve CreatedAt — do NOT overwrite it
        var existing = await _context.JournalEntries.AsNoTracking()
            .FirstOrDefaultAsync(j => j.JournalEntryId == id);

        if (existing == null)
            return NotFound();

        model.CreatedAt = existing.CreatedAt;

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var entry = await _context.JournalEntries.FindAsync(id);
        if (entry == null)
            return NotFound();

        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
