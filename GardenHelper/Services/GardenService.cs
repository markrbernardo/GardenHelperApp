using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Enums;
using GardenHelperApp.Shared.Constants;
using System.Net.Http.Json;

namespace GardenHelperApp.Client.Services;

public class GardenService
{
    private readonly HttpClient _http;

    public GardenService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL GARDENS FOR A USER
    // ---------------------------------------------------------
    public async Task<List<GardenModel>> GetByUserAsync(int userId)
    {
        var url = ApiRoutes.Gardens.ByUser.Replace("{userId}", userId.ToString());

        return await _http.GetFromJsonAsync<List<GardenModel>>(url)
               ?? new List<GardenModel>();
    }

    // ---------------------------------------------------------
    // GET SINGLE GARDEN
    // ---------------------------------------------------------
    public async Task<GardenModel?> GetAsync(int id)
    {
        var url = ApiRoutes.Gardens.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<GardenModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // CREATE GARDEN
    // ---------------------------------------------------------
    public async Task<GardenModel?> CreateAsync(GardenModel model)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.Gardens.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<GardenModel>();
    }

    // ---------------------------------------------------------
    // UPDATE GARDEN
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(GardenModel model)
    {
        var url = ApiRoutes.Gardens.ById.Replace("{id}", model.GardenId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE GARDEN
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.Gardens.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // SET DEFAULT GARDEN FOR USER
    // ---------------------------------------------------------
    public async Task<bool> SetDefaultGardenAsync(int userId, int gardenId)
    {
        var url = ApiRoutes.Gardens.SetDefaultGarden
            .Replace("{userId}", userId.ToString())
            .Replace("{gardenId}", gardenId.ToString());

        var response = await _http.PutAsync(url, null);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // GET DEFAULT GARDEN FOR USER
    // ---------------------------------------------------------
    public async Task<int?> GetDefaultGardenAsync(int userId)
    {
        var url = ApiRoutes.Gardens.DefaultGarden.Replace("{userId}", userId.ToString());

        return await _http.GetFromJsonAsync<int?>(url);
    }
}
