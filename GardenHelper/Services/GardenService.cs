using GardenHelper.Pages;
using GardenHelperApp.Shared.Models;
using System.Net.Http.Json;

namespace GardenHelperApp.Client.Services;

public class GardenService
{
    private readonly HttpClient _http;

    public GardenService(HttpClient http)
    {
        _http = http;
    }

    // REQUIRED: Get gardens for a specific user
    public async Task<List<GardenModel>> GetGardensByUser(int userId)
    {
        return await _http.GetFromJsonAsync<List<GardenModel>>($"api/gardens/user/{userId}")
               ?? new List<GardenModel>();
    }

    // Get a single garden
    public async Task<GardenModel?> GetGarden(int id)
    {
        return await _http.GetFromJsonAsync<GardenModel>($"api/gardens/{id}");
    }

    public async Task<List<GardenModel?>> GetAllGardens()
    {
        var gardens = await _http.GetFromJsonAsync<List<GardenModel>>("api/gardens/");
        return gardens ?? new List<GardenModel>();
    }

    // Create a garden
    public async Task CreateGarden(GardenModel garden)
    {
        await _http.PostAsJsonAsync("api/gardens", garden);
    }

    // Update a garden
    public async Task UpdateGarden(GardenModel garden)
    {
        await _http.PutAsJsonAsync($"api/gardens/{garden.GardenId}", garden);
    }

    // Delete a garden
    public async Task DeleteGarden(int id)
    {
        await _http.DeleteAsync($"api/gardens/{id}");
    }

    // Get locations for a garden
    public async Task<List<LocationModel>> GetLocationsByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<LocationModel>>(
            $"api/locations/garden/{gardenId}"
        ) ?? new List<LocationModel>();
    }

    // Get plants for a garden
    public async Task<List<PlantModel>> GetPlantsByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>(
            $"api/plants/garden/{gardenId}"
        ) ?? new List<PlantModel>();
    }



}
