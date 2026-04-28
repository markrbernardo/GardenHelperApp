using Microsoft.AspNetCore.Mvc;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace GardenHelperApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantPhotosController : ControllerBase
{
    private readonly GardenContext _db;

    public PlantPhotosController(GardenContext db)
    {
        _db = db;
    }

    // GET: api/plantphotos/plant/3
    [HttpGet("plant/{plantId}")]
    public async Task<IActionResult> GetPhotosForPlant(int plantId)
    {
        try
        {
            var photos = await _db.PlantPhotos
                .Where(p => p.PlantId == plantId)
                .OrderByDescending(p => p.PhotoId)
                .ToListAsync();

            return Ok(photos);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR in GetPhotosForPlant: " + ex);
            return StatusCode(500, ex.Message);
        }
    }

    // POST: api/plantphotos
    [HttpPost]
    public async Task<IActionResult> AddPhoto(PlantPhotoModel model)
    {
        try
        {
            model.CreatedAt = DateTime.UtcNow.ToString("o");

            _db.PlantPhotos.Add(model);
            await _db.SaveChangesAsync();

            return Ok(model);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR in AddPhoto: " + ex);
            return StatusCode(500, ex.Message);
        }
    }

    // DELETE: api/plantphotos/5
    [HttpDelete("{photoId}")]
    public async Task<IActionResult> DeletePhoto(int photoId)
    {
        var photo = await _db.PlantPhotos.FindAsync(photoId);

        if (photo == null)
            return NotFound();

        _db.PlantPhotos.Remove(photo);
        await _db.SaveChangesAsync();

        return Ok();
    }
}
