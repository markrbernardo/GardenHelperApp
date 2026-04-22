using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalEntriesController : ControllerBase
{
    private readonly GardenContext _context;

    public JournalEntriesController(GardenContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<JournalEntryModel>>> GetAll()
    {
        return await _context.JournalEntries.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JournalEntryModel>> Get(int id)
    {
        var entry = await _context.JournalEntries.FindAsync(id);
        if (entry == null) return NotFound();
        return entry;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByUser(int userId)
    {
        return await _context.JournalEntries
            .Where(j => j.UserId == userId)
            .ToListAsync();
    }

    [HttpGet("garden/{gardenId}")]
    public async Task<ActionResult<List<JournalEntryModel>>> GetByGarden(int gardenId)
    {
        return await _context.JournalEntries
            .Where(j => j.GardenId == gardenId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JournalEntryModel model)
    {
        _context.JournalEntries.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, JournalEntryModel model)
    {
        if (id != model.JournalEntryId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entry = await _context.JournalEntries.FindAsync(id);
        if (entry == null) return NotFound();

        _context.JournalEntries.Remove(entry);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
