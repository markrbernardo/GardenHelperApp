using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class JournalEntryService
{
    private readonly HttpClient _http;

    public JournalEntryService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL (rarely used)
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(ApiRoutes.JournalEntries.Base)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    public async Task<JournalEntryModel?> GetAsync(int id)
    {
        var url = ApiRoutes.JournalEntries.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<JournalEntryModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // GET BY USER
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetByUserAsync(int userId)
    {
        var url = ApiRoutes.JournalEntries.ByUser.Replace("{userId}", userId.ToString());

        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(url)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // GET BY GARDEN
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetByGardenAsync(int gardenId)
    {
        var url = ApiRoutes.JournalEntries.ByGarden.Replace("{gardenId}", gardenId.ToString());

        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(url)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // GET BY DATE RANGE (ALL JOURNAL ENTRIES)
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        var url = $"{ApiRoutes.JournalEntries.Range}?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(url)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // GET BY USER + DATE RANGE
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetByUserAndDateRangeAsync(
        int userId,
        DateTime start,
        DateTime end)
    {
        var url = ApiRoutes.JournalEntries.UserRange
            .Replace("{userId}", userId.ToString());

        url += $"?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(url)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // GET BY GARDEN + DATE RANGE
    // ---------------------------------------------------------
    public async Task<List<JournalEntryModel>> GetByGardenAndDateRangeAsync(
        int gardenId,
        DateTime start,
        DateTime end)
    {
        var url = ApiRoutes.JournalEntries.GardenRange
            .Replace("{gardenId}", gardenId.ToString());

        url += $"?start={start:O}&end={end:O}";

        return await _http.GetFromJsonAsync<List<JournalEntryModel>>(url)
               ?? new List<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    public async Task<JournalEntryModel?> CreateAsync(JournalEntryModel model)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.JournalEntries.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<JournalEntryModel>();
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(JournalEntryModel model)
    {
        var url = ApiRoutes.JournalEntries.ById.Replace("{id}", model.JournalEntryId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.JournalEntries.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }
}
