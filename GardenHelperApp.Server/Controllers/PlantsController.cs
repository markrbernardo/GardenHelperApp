using GardenHelperApp.Server.Data;
using GardenHelperApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly GardenContext _context;

    public PlantsController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // INTERNAL PROJECTION (shared by all DTO endpoints)
    // ---------------------------------------------------------
    private IQueryable<PlantWithInfoDto> ProjectToDto()
    {
        return from p in _context.Plants
               join i in _context.PlantInformation on p.PlantInformationId equals i.PlantInformationId
               join l in _context.Locations on p.LocationId equals l.LocationId
               select new PlantWithInfoDto
               {
                   PlantId = p.PlantId,
                   Name = p.Name,
                   PlantInformationId = p.PlantInformationId,
                   ScientificName = i.ScientificName,
                   CommonName = i.CommonName,
                   LocationId = p.LocationId,
                   LocationName = l.Name,
                   GardenId = p.GardenId,

                   Description = p.Description,
                   Notes = p.Notes,

                   // ENUM FIELDS
                   GrowingSeason = i.GrowingSeason,
                   Light = i.Light,
                   Water = i.Water,
                   Soil = i.Soil,
                   Container = i.Container,
                   Fertilization = i.Fertilization,
                   Propagation = i.Propagation,
                   Health = i.Health,

                   // FREE-FORM FIELDS
                   Seeds = i.Seeds,
                   Air = i.Air,
                   Pruning = i.Pruning,

                   // PHOTOS
                   Photo = i.Photo,
                   PhotoMimeType = i.PhotoMimeType
               };
    }


    // ---------------------------------------------------------
    // GET WITH INFO
    // ---------------------------------------------------------
    [HttpGet("{id}/withinfo")]
    public async Task<ActionResult<PlantWithInfoDto>> GetWithInfoAsync(int id)
    {
        var plant = await ProjectToDto()
            .Where(p => p.PlantId == id)
            .FirstOrDefaultAsync();

        if (plant == null)
            return NotFound();

        return Ok(plant);
    }



    // ---------------------------------------------------------
    // GET ALL (DTO)
    // ---------------------------------------------------------
    [HttpGet]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetAllAsync()
    {
        var plants = await ProjectToDto()
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return plants;
    }

    // ---------------------------------------------------------
    // GET SINGLE (MODEL)
    // ---------------------------------------------------------
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlantModel>> GetAsync(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null)
            return NotFound();

        return plant;
    }

    // ---------------------------------------------------------
    // GET BY GARDEN (DTO)
    // ---------------------------------------------------------
    [HttpGet("garden/{gardenId:int}")]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetByGardenAsync(int gardenId)
    {
        var plants = await ProjectToDto()
            .Where(p => p.GardenId == gardenId)
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return plants;
    }

    // ---------------------------------------------------------
    // GET BY LOCATION (DTO)
    // ---------------------------------------------------------
    [HttpGet("location/{locationId:int}")]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetByLocationAsync(int locationId)
    {
        var plants = await ProjectToDto()
            .Where(p => p.LocationId == locationId)
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return plants;
    }

    // ---------------------------------------------------------
    // GET BY PLANT INFO ID (MODEL)
    // ---------------------------------------------------------
    [HttpGet("info/{plantInfoId:int}")]
    public async Task<ActionResult<List<PlantModel>>> GetByPlantInfoAsync(int plantInfoId)
    {
        var plants = await _context.Plants
            .Where(p => p.PlantInformationId == plantInfoId)
            .ToListAsync();

        return plants;
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<PlantModel>> CreateAsync(PlantModel model)
    {
        _context.Plants.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAsync), new { id = model.PlantId }, model);
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, PlantModel model)
    {
        if (id != model.PlantId)
            return BadRequest("Plant ID mismatch.");

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
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null)
            return NotFound();

        _context.Plants.Remove(plant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
