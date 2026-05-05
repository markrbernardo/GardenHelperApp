using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantInformationController : ControllerBase
{
    private readonly GardenContext _context;

    public PlantInformationController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET ALL
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<PlantInformationModel>>> GetAllAsync()
    {
        return await _context.PlantInformation
            .OrderBy(pi => pi.ScientificName ?? pi.CommonName)
            .ToListAsync();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlantInformationModel>> GetAsync(int id)
    {
        var info = await _context.PlantInformation.FindAsync(id);
        if (info == null)
            return NotFound();

        return info;
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<PlantInformationModel>> CreateAsync(PlantInformationModel model)
    {
        _context.PlantInformation.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsync), new { id = model.PlantInformationId }, model);
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, PlantInformationModel model)
    {
        if (id != model.PlantInformationId)
            return BadRequest("PlantInformation ID mismatch.");

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
        var info = await _context.PlantInformation.FindAsync(id);
        if (info == null)
            return NotFound();

        _context.PlantInformation.Remove(info);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
