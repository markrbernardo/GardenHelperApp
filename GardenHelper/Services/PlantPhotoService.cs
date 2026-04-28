using GardenHelperApp.Shared.Models;
using System.Net.Http.Json;

namespace GardenHelperApp.Client.Services;

public class PlantPhotoService
{
    private readonly HttpClient _http;

    public PlantPhotoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PlantPhotoModel>> GetPhotosForPlant(int plantId)
    {
        return await _http.GetFromJsonAsync<List<PlantPhotoModel>>(
            $"api/plantphotos/plant/{plantId}"
        ) ?? new List<PlantPhotoModel>();
    }

    public async Task AddPhoto(PlantPhotoModel model)
    {
        await _http.PostAsJsonAsync("api/plantphotos", model);
    }

    public async Task DeletePhoto(int photoId)
    {
        await _http.DeleteAsync($"api/plantphotos/{photoId}");
    }
}
