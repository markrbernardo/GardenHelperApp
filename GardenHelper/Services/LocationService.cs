using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Enums;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class LocationService
{
    private readonly HttpClient _http;

    public LocationService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET SINGLE LOCATION
    // ---------------------------------------------------------
    public async Task<LocationModel?> GetAsync(int id)
    {
        var url = ApiRoutes.Locations.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<LocationModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // GET LOCATIONS BY GARDEN
    // ---------------------------------------------------------
    public async Task<List<LocationModel>> GetByGardenAsync(int gardenId)
    {
        var url = ApiRoutes.Locations.ByGarden.Replace("{gardenId}", gardenId.ToString());

        return await _http.GetFromJsonAsync<List<LocationModel>>(url)
               ?? new List<LocationModel>();
    }

    // ---------------------------------------------------------
    // CREATE LOCATION
    // ---------------------------------------------------------
    public async Task<int?> CreateAsync(LocationModel model)
    {
        // Ensure enum defaults
        if (!Enum.IsDefined(typeof(LocationLighting), model.Lighting))
            model.Lighting = LocationLighting.Unknown;

        var response = await _http.PostAsJsonAsync(ApiRoutes.Locations.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<int>();
    }

    // ---------------------------------------------------------
    // UPDATE LOCATION
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(LocationModel model)
    {
        // Ensure enum defaults
        if (!Enum.IsDefined(typeof(LocationLighting), model.Lighting))
            model.Lighting = LocationLighting.Unknown;

        var url = ApiRoutes.Locations.ById.Replace("{id}", model.LocationId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE LOCATION
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.Locations.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }
}
