using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Client.Services;

public class ObservationService
{
    private readonly HttpClient _http;

    public ObservationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ObservationModel>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<ObservationModel>>("api/observations")
               ?? new List<ObservationModel>();
    }

    public async Task<ObservationModel?> Get(int id)
    {
        return await _http.GetFromJsonAsync<ObservationModel>($"api/observations/{id}");
    }

    public async Task<List<ObservationModel>> GetByPlant(int plantId)
    {
        return await _http.GetFromJsonAsync<List<ObservationModel>>($"api/observations/plant/{plantId}")
               ?? new List<ObservationModel>();
    }

    // CREATE with timestamps
    public async Task<int> Create(ObservationModel model)
    {
        model.CreatedAt = DateTime.Now;
        model.UpdatedAt = DateTime.Now;

        var response = await _http.PostAsJsonAsync("api/observations", model);
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task Update(ObservationModel model)
    {
        model.UpdatedAt = DateTime.Now;
        await _http.PutAsJsonAsync($"api/observations/{model.ObservationId}", model);
    }


    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/observations/{id}");
    }

    public async Task UpdateActiveStatus(int id, bool isActive)
    {
        await _http.PutAsJsonAsync($"api/observations/{id}/active", isActive);
    }



}
