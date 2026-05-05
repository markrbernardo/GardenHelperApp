using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;
using GardenHelperApp.Shared.Constants;

namespace GardenHelperApp.Client.Services;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    // ---------------------------------------------------------
    // GET ALL USERS
    // ---------------------------------------------------------
    public async Task<List<UserModel>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<UserModel>>(ApiRoutes.Users.Base)
               ?? new List<UserModel>();
    }

    // ---------------------------------------------------------
    // GET SINGLE USER
    // ---------------------------------------------------------
    public async Task<UserModel?> GetAsync(int id)
    {
        var url = ApiRoutes.Users.ById.Replace("{id}", id.ToString());

        try
        {
            return await _http.GetFromJsonAsync<UserModel>(url);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    // ---------------------------------------------------------
    // CREATE USER
    // ---------------------------------------------------------
    public async Task<UserModel?> CreateAsync(UserModel user)
    {
        var response = await _http.PostAsJsonAsync(ApiRoutes.Users.Base, user);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserModel>();
    }

    // ---------------------------------------------------------
    // UPDATE USER
    // ---------------------------------------------------------
    public async Task<bool> UpdateAsync(UserModel user)
    {
        var url = ApiRoutes.Users.ById.Replace("{id}", user.UserId.ToString());

        var response = await _http.PutAsJsonAsync(url, user);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DELETE USER
    // ---------------------------------------------------------
    public async Task<bool> DeleteAsync(int id)
    {
        var url = ApiRoutes.Users.ById.Replace("{id}", id.ToString());

        var response = await _http.DeleteAsync(url);

        return response.IsSuccessStatusCode;
    }

    // ---------------------------------------------------------
    // DEFAULT GARDEN
    // ---------------------------------------------------------
    public async Task<bool> SetDefaultGardenAsync(int userId, int gardenId)
    {
        var url = ApiRoutes.Gardens.SetDefaultGarden
            .Replace("{userId}", userId.ToString())
            .Replace("{gardenId}", gardenId.ToString());

        var response = await _http.PutAsync(url, null);

        return response.IsSuccessStatusCode;
    }

    public async Task<int?> GetDefaultGardenAsync(int userId)
    {
        var url = ApiRoutes.Gardens.DefaultGarden.Replace("{userId}", userId.ToString());

        return await _http.GetFromJsonAsync<int?>(url);
    }
}
