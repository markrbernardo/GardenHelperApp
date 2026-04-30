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

    // ---------------------------
    // GET ALL PLANTS (DTO)
    // ---------------------------
    [HttpGet]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetAll()
    {
        var result =
            await (from p in _context.Plants
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
                       LocationName = l.Name,   // ✔ FIXED
                       GardenId = p.GardenId,

                       Description = p.Description,
                       Notes = p.Notes,

                       GrowingSeason = i.GrowingSeason,
                       Photo = i.Photo,
                       PhotoMimeType = i.PhotoMimeType,
                       Seeds = i.Seeds,
                       Light = i.Light,
                       Water = i.Water,
                       Air = i.Air,
                       Soil = i.Soil,
                       Container = i.Container,
                       Fertilization = i.Fertilization,
                       Pruning = i.Pruning,
                       Propagation = i.Propagation,
                       Health = i.Health
                   })
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return Ok(result);
    }


    // ---------------------------
    // GET SINGLE PLANT (MODEL)
    // ---------------------------
    [HttpGet("{id}")]
    public async Task<ActionResult<PlantModel>> Get(int id)
    {
        var plant = await _context.Plants.FindAsync(id);
        if (plant == null) return NotFound();
        return plant;
    }

    // ---------------------------
    // GET PLANTS BY GARDEN (DTO)
    // ---------------------------
    [HttpGet("garden/{gardenId}")]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetByGarden(int gardenId)
    {
        var result =
            await (from p in _context.Plants
                   join i in _context.PlantInformation on p.PlantInformationId equals i.PlantInformationId
                   join l in _context.Locations on p.LocationId equals l.LocationId
                   where p.GardenId == gardenId
                   select new PlantWithInfoDto
                   {
                       PlantId = p.PlantId,
                       Name = p.Name,
                       PlantInformationId = p.PlantInformationId,
                       ScientificName = i.ScientificName,
                       CommonName = i.CommonName,
                       LocationId = p.LocationId,
                       LocationName = l.Name,   // ✔ FIXED
                       GardenId = p.GardenId,

                       Description = p.Description,
                       Notes = p.Notes,

                       GrowingSeason = i.GrowingSeason,
                       Photo = i.Photo,
                       PhotoMimeType = i.PhotoMimeType,
                       Seeds = i.Seeds,
                       Light = i.Light,
                       Water = i.Water,
                       Air = i.Air,
                       Soil = i.Soil,
                       Container = i.Container,
                       Fertilization = i.Fertilization,
                       Pruning = i.Pruning,
                       Propagation = i.Propagation,
                       Health = i.Health
                   })
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return Ok(result);
    }


    // ---------------------------
    // GET PLANTS BY LOCATION (DTO)
    // ---------------------------
    [HttpGet("location/{locationId}")]
    public async Task<ActionResult<List<PlantWithInfoDto>>> GetByLocation(int locationId)
    {
        var result =
            await (from p in _context.Plants
                   join i in _context.PlantInformation on p.PlantInformationId equals i.PlantInformationId
                   join l in _context.Locations on p.LocationId equals l.LocationId
                   where p.LocationId == locationId
                   select new PlantWithInfoDto
                   {
                       PlantId = p.PlantId,
                       Name = p.Name,
                       PlantInformationId = p.PlantInformationId,
                       ScientificName = i.ScientificName,
                       CommonName = i.CommonName,
                       LocationId = p.LocationId,
                       LocationName = l.Name,   // ✔ FIXED
                       GardenId = p.GardenId,

                       Description = p.Description,
                       Notes = p.Notes,

                       GrowingSeason = i.GrowingSeason,
                       Photo = i.Photo,
                       PhotoMimeType = i.PhotoMimeType,
                       Seeds = i.Seeds,
                       Light = i.Light,
                       Water = i.Water,
                       Air = i.Air,
                       Soil = i.Soil,
                       Container = i.Container,
                       Fertilization = i.Fertilization,
                       Pruning = i.Pruning,
                       Propagation = i.Propagation,
                       Health = i.Health
                   })
            .OrderBy(x => x.ScientificName ?? x.CommonName)
            .ToListAsync();

        return Ok(result);
    }


    // ---------------------------
    // GET PLANTS BY PLANT INFO ID (MODEL)
    // ---------------------------
    [HttpGet("info/{plantInfoId}")]
    public async Task<ActionResult<List<PlantModel>>> GetByPlantInfo(int plantInfoId)
    {
        return await _context.Plants
            .Where(p => p.PlantInformationId == plantInfoId)
            .ToListAsync();
    }

    // ---------------------------
    // CREATE
    // ---------------------------
    [HttpPost]
    public async Task<IActionResult> Create(PlantModel model)
    {
        _context.Plants.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    // ---------------------------
    // UPDATE
    // ---------------------------
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PlantModel model)
    {
        if (id != model.PlantId) return BadRequest();

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // ---------------------------
    // DELETE
    // ---------------------------
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
