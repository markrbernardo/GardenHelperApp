using GardenHelperApp.Shared.Models;
using System.Net.Http.Json;

namespace GardenHelperApp.Client.Services;

public class PlantService
{
    private readonly HttpClient _http;

    public PlantService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PlantModel>> GetAllPlants()
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>("api/plants")
               ?? new List<PlantModel>();
    }

    public async Task<PlantModel?> GetPlant(int id)
    {
        return await _http.GetFromJsonAsync<PlantModel>($"api/plants/{id}");
    }

    public async Task<List<PlantModel>> GetPlantsByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>($"api/plants/garden/{gardenId}");
    }

    public async Task<List<PlantModel>> GetPlantsByLocation(int locationId)
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>($"api/plants/location/{locationId}");
    }

    public async Task<List<PlantModel>> GetByLocation(int locationId)
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>($"api/plants/location/{locationId}")
               ?? new List<PlantModel>();
    }

    public async Task<List<PlantModel>> GetByPlantInfo(int plantInfoId)
    {
        return await _http.GetFromJsonAsync<List<PlantModel>>($"api/plants/info/{plantInfoId}")
               ?? new List<PlantModel>();
    }

    // ✔ Correct endpoint
    public async Task Create(PlantModel model)
    {
        await _http.PostAsJsonAsync("api/plants/create", model);
    }

    // ✔ Correct endpoint + returns response for debugging
    public async Task<HttpResponseMessage> CreateWithResponse(PlantModel model)
    {
        return await _http.PostAsJsonAsync("api/plants/create", model);
    }

    public async Task Update(PlantModel model)
    {
        await _http.PutAsJsonAsync($"api/plants/{model.PlantId}", model);
    }

    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/plants/{id}");
    }
}
