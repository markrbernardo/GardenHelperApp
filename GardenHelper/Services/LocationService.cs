using GardenHelperApp.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

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

    public async Task<int> Create(LocationModel location)
    {
        var response = await _http.PostAsJsonAsync("api/locations", location);
        return await response.Content.ReadFromJsonAsync<int>();
    }



    public async Task Update(LocationModel model)
    {
        await _http.PutAsJsonAsync($"api/locations/{model.LocationId}", model);
    }

    public async Task Delete(int id)
    {
        await _http.DeleteAsync($"api/locations/{id}");
    }



    public async Task<LocationModel?> GetLocation(int id)
    {
        return await _http.GetFromJsonAsync<LocationModel>($"api/locations/{id}");
    }


    public async Task<List<LocationModel>> GetLocationsByGarden(int gardenId)
    {
        return await _http.GetFromJsonAsync<List<LocationModel>>(
            $"api/locations/garden/{gardenId}"
        ) ?? new List<LocationModel>();
    }

    public async Task UpdateLocation(LocationModel location)
    {
        await _http.PutAsJsonAsync($"api/locations/{location.LocationId}", location);
    }


}
