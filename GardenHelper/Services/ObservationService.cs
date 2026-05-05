using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class ObservationService
{
    private readonly HttpClient _http;

    public ObservationService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL (rarely used)
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<ObservationModel>>(ApiRoutes.Observations.Base)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    public async Task<ObservationModel?> GetAsync(int id)
    {
        var url = ApiRoutes.Observations.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<ObservationModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // GET BY PLANT
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetByPlantAsync(int plantId)
    {
        var url = ApiRoutes.Observations.ByPlant.Replace("{plantId}", plantId.ToString());

        return await _http.GetFromJsonAsync<List<ObservationModel>>(url)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // GET BY USER
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetByUserAsync(int userId)
    {
        var url = ApiRoutes.Observations.ByUser.Replace("{userId}", userId.ToString());

        return await _http.GetFromJsonAsync<List<ObservationModel>>(url)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // GET BY DATE RANGE (ALL OBSERVATIONS)
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        var url = $"{ApiRoutes.Observations.Range}?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<ObservationModel>>(url)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // GET BY PLANT + DATE RANGE
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetByPlantAndDateRangeAsync(
        int plantId,
        DateTime start,
        DateTime end)
    {
        var url = ApiRoutes.Observations.PlantRange
            .Replace("{plantId}", plantId.ToString());

        url += $"?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<ObservationModel>>(url)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // GET BY USER + DATE RANGE
    // ---------------------------------------------------------
    public async Task<List<ObservationModel>> GetByUserAndDateRangeAsync(
        int userId,
        DateTime start,
        DateTime end)
    {
        var url = ApiRoutes.Observations.UserRange
            .Replace("{userId}", userId.ToString());

        url += $"?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<ObservationModel>>(url)
               ?? new List<ObservationModel>();
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    public async Task<int?> CreateAsync(ObservationModel model)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.Observations.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<int>();
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(ObservationModel model)
    {
        var url = ApiRoutes.Observations.ById.Replace("{id}", model.ObservationId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.Observations.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // UPDATE ACTIVE STATUS
    // ---------------------------------------------------------
    public async Task<bool> UpdateActiveStatusAsync(int id, bool isActive)
    {
        var url = ApiRoutes.Observations.ActiveStatus.Replace("{id}", id.ToString());

        var response = await _http.PutAsJsonAsync(url, isActive);

        return response.IsSuccessStatusCode;
    }
}
