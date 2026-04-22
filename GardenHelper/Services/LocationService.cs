using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Client.Services;

public class LocationService
{
    private readonly HttpClient _http;

    public LocationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<LocationModel>> GetAll()
    {
        return await _http.GetFromJsonAsync<List<LocationModel>>("api/locations")
               ?? new List<LocationModel>();
    }

    public async Task<LocationModel?> Get(int id)
    {
        return await _http.GetFromJsonAsync<LocationModel>($"api/locations/{id}");
    }

    public async Task<List<LocationModel>> GetByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<LocationModel>>($"api/locations/garden/{gardenId}")
               ?? new List<LocationModel>();
    }

    public async Task Create(LocationModel model)
    {
        await _http.PostAsJsonAsync("api/locations", model);
    }

    public async Task Update(LocationModel model)
    {
        await _http.PutAsJsonAsync($"api/locations/{model.LocationId}", model);
    }

    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/locations/{id}");
    }
}
