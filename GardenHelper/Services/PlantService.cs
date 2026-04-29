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

    // ---------------------------------------------------------
    // GET ALL PLANTS (already joined + sorted by server)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetAllPlants()
    {
        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>("api/plants")
               ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET PLANTS BY GARDEN (already joined + sorted by server)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetPlantsByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>(
            $"api/plants/garden/{gardenId}"
        ) ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET PLANTS BY LOCATION (already joined + sorted by server)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetPlantsByLocation(int locationId)
    {
        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>(
            $"api/plants/location/{locationId}"
        ) ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET SINGLE PLANT (still returns PlantModel)
    // ---------------------------------------------------------
    public async Task<PlantModel?> GetPlant(int id)
    {
        return await _http.GetFromJsonAsync<PlantModel>($"api/plants/{id}");
    }

    // ---------------------------------------------------------
    // CRUD
    // ---------------------------------------------------------
    public async Task Create(PlantModel model)
    {
        await _http.PostAsJsonAsync("api/plants/create", model);
    }

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
