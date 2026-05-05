using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Enums;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class PlantInformationService
{
    private readonly HttpClient _http;

    public PlantInformationService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL
    // ---------------------------------------------------------
    public async Task<List<PlantInformationModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<PlantInformationModel>>(ApiRoutes.PlantInformation.Base)
               ?? new List<PlantInformationModel>();
    }

    // ---------------------------------------------------------
    // GET SINGLE
    // ---------------------------------------------------------
    public async Task<PlantInformationModel?> GetAsync(int id)
    {
        var url = ApiRoutes.PlantInformation.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<PlantInformationModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // CREATE
    // ---------------------------------------------------------
    public async Task<PlantInformationModel?> CreateAsync(PlantInformationModel model)
    {
        EnsureEnumDefaults(model);

        var response = await _http.PostAsJsonAsync(ApiRoutes.PlantInformation.Base, model);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<PlantInformationModel>();
    }

    // ---------------------------------------------------------
    // UPDATE
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(PlantInformationModel model)
    {
        EnsureEnumDefaults(model);

        var url = ApiRoutes.PlantInformation.ById.Replace("{id}", model.PlantInformationId.ToString());

        var response = await _http.PutAsJsonAsync(url, model);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.PlantInformation.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // ENUM SAFETY
    // ---------------------------------------------------------
    private static void EnsureEnumDefaults(PlantInformationModel model)
    {
        model.GrowingSeason = model.GrowingSeason == 0 ? Season.Unknown : model.GrowingSeason;
        model.Light = model.Light == 0 ? LightRequirement.Unknown : model.Light;
        model.Water = model.Water == 0 ? WaterRequirement.Unknown : model.Water;
        model.Soil = model.Soil == 0 ? SoilType.Unknown : model.Soil;
        model.Container = model.Container == 0 ? ContainerType.Unknown : model.Container;
        model.Fertilization = model.Fertilization == 0 ? FertilizerType.Unknown : model.Fertilization;
        model.Propagation = model.Propagation == 0 ? PropagationMethod.Unknown : model.Propagation;
        model.Health = model.Health == 0 ? PlantHealthStatus.Unknown : model.Health;
    }
}
