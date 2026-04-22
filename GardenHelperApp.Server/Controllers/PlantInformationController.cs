using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Server.Data;

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

    [HttpGet]
    public async Task<ActionResult<List<PlantInformationModel>>> GetAll()
    {
        return await _context.PlantInformation.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlantInformationModel>> Get(int id)
    {
        var plant = await _context.PlantInformation.FindAsync(id);
        if (plant == null) return NotFound();
        return plant;
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlantInformationModel model)
    {
        _context.PlantInformation.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PlantInformationModel model)
    {
        if (id != model.PlantInformationId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _context.PlantInformation.FindAsync(id);
        if (plant == null) return NotFound();

        _context.PlantInformation.Remove(plant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
