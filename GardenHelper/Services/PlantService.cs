using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class PlantService
{
    private readonly HttpClient _http;

    public PlantService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL (PlantWithInfoDto)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>(ApiRoutes.Plants.Base)
               ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET BY GARDEN (PlantWithInfoDto)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetByGardenAsync(int gardenId)
    {
        var url = ApiRoutes.Plants.ByGarden.Replace("{gardenId}", gardenId.ToString());

        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>(url)
               ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET BY LOCATION (PlantWithInfoDto)
    // ---------------------------------------------------------
    public async Task<List<PlantWithInfoDto>> GetByLocationAsync(int locationId)
    {
        var url = ApiRoutes.Plants.ByLocation.Replace("{locationId}", locationId.ToString());

        return await _http.GetFromJsonAsync<List<PlantWithInfoDto>>(url)
               ?? new List<PlantWithInfoDto>();
    }

    // ---------------------------------------------------------
    // GET WITH INFO (PlantWithInfoDto)
    // ---------------------------------------------------------
    public async Task<PlantWithInfoDto?> GetWithInfoAsync(int id)
    {
        var url = ApiRoutes.Plants.WithInfo.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<PlantWithInfoDto>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }


    // ---------------------------------------------------------
    // GET SINGLE PLANT (PlantModel)
    // ---------------------------------------------------------
    public async Task<PlantModel?> GetAsync(int id)
    {
        var url = ApiRoutes.Plants.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<PlantModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // CREATE (returns created model)
    // ---------------------------------------------------------
    public async Task<PlantModel?> CreateAsync(PlantModel model)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.Plants.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PlantModel>();
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(PlantModel model)
    {
        var url = ApiRoutes.Plants.ById.Replace("{id}", model.PlantId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.Plants.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }
}
