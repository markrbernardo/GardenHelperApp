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

    public async Task Create(ObservationModel model)
    {
        await _http.PostAsJsonAsync("api/observations", model);
    }

    public async Task Update(ObservationModel model)
    {
        await _http.PutAsJsonAsync($"api/observations/{model.ObservationId}", model);
    }

    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/observations/{id}");
    }
}
