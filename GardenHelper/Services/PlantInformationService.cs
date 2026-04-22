using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Client.Services;

public class PlantInformationService
{
    private readonly HttpClient _http;

    public PlantInformationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PlantInformationModel>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<PlantInformationModel>>("api/plantinformation")
               ?? new List<PlantInformationModel>();
    }

    public async Task<PlantInformationModel?> Get(int id)
    {
        return await _http.GetFromJsonAsync<PlantInformationModel>($"api/plantinformation/{id}");
    }

    public async Task Create(PlantInformationModel model)
    {
        await _http.PostAsJsonAsync("api/plantinformation", model);
    }

    public async Task Update(PlantInformationModel model)
    {
        await _http.PutAsJsonAsync($"api/plantinformation/{model.PlantInformationId}", model);
    }

    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/plantinformation/{id}");
    }
}
