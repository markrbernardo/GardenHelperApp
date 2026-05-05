using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Server.Data;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantPhotosController : ControllerBase
{
    private readonly GardenContext _context;

    public PlantPhotosController(GardenContext context)
    {
        _context = context;
    }

    // ---------------------------------------------------------
    // GET PHOTOS BY PLANT
    // ---------------------------------------------------------
    [HttpGet("plant/{plantId:int}")]
    public async Task<ActionResult<List<PlantPhotoModel>>> GetByPlantAsync(int plantId)
    {
        var photos = await _context.PlantPhotos
            .Where(p => p.PlantId == plantId)
            .OrderByDescending(p => p.PhotoId)
            .ToListAsync();

        return photos;
    }

    // ---------------------------------------------------------
    // CREATE PHOTO
    // ---------------------------------------------------------
    [HttpPost]
    public async Task<ActionResult<PlantPhotoModel>> CreateAsync(PlantPhotoModel model)
    {
        // Server controls timestamps
        model.CreatedAt = DateTime.UtcNow;

        _context.PlantPhotos.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByPlantAsync), new { plantId = model.PlantId }, model);
    }


    // ---------------------------------------------------------
    // DELETE PHOTO
    // ---------------------------------------------------------
    [HttpDelete("{photoId:int}")]
    public async Task<IActionResult> DeleteAsync(int photoId)
    {
        var photo = await _context.PlantPhotos.FindAsync(photoId);
        if (photo == null)
            return NotFound();

        _context.PlantPhotos.Remove(photo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
