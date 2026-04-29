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
        var list = await _http.GetFromJsonAsync<List<PlantInformationModel>>("api/plantinformation")
                   ?? new List<PlantInformationModel>();

        return list
            .OrderBy(pi => pi.ScientificName ?? pi.CommonName)
            .ToList();
    }


    public async Task<PlantInformationModel?> Get(int id)
    {
        return await _http.GetFromJsonAsync<PlantInformationModel>($"api/plantinformation/{id}");
    }

    public async Task Create(PlantInformationModel model)
    {
        var resp = await _http.PostAsJsonAsync("api/plantinformation", model);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            throw new Exception($"Create failed: {err}");
        }
    }

    public async Task Update(PlantInformationModel model)
    {
        var resp = await _http.PutAsJsonAsync($"api/plantinformation/{model.PlantInformationId}", model);
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {err}");
        }
    }

    public async Task Delete(int id)
    {
        var resp = await _http.DeleteAsync($"api/plantinformation/{id}");
        if (!resp.IsSuccessStatusCode)
        {
            var err = await resp.Content.ReadAsStringAsync();
            throw new Exception($"Delete failed: {err}");
        }
    }

    public async Task<PlantInformationModel?> GetById(int id)
    {
        return await _http.GetFromJsonAsync<PlantInformationModel>(
            $"api/plantinformation/{id}"
        );
    }
}
