using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class PlantPhotoService
{
    private readonly HttpClient _http;

    public PlantPhotoService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET PHOTOS BY PLANT
    // ---------------------------------------------------------
    public async Task<List<PlantPhotoModel>> GetByPlantAsync(int plantId)
    {
        var url = ApiRoutes.PlantPhotos.ByPlant.Replace("{plantId}", plantId.ToString());

        return await _http.GetFromJsonAsync<List<PlantPhotoModel>>(url)
               ?? new List<PlantPhotoModel>();
    }

    // ---------------------------------------------------------
    // CREATE PHOTO
    // ---------------------------------------------------------
    public async Task CreateAsync(PlantPhotoModel model)
    {
        await _http.PostAsJsonAsync(ApiRoutes.PlantPhotos.Base, model);
    }

    // ---------------------------------------------------------
    // DELETE PHOTO
    // ---------------------------------------------------------
    public async Task DeleteAsync(int photoId)
    {
        var url = ApiRoutes.PlantPhotos.ById.Replace("{photoId}", photoId.ToString());

        await _http.DeleteAsync(url);
    }
}
