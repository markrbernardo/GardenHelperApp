using GardenHelperApp.Shared.Models;
using System.Net.Http.Json;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserModel>> GetUsers()
    {
        var users = await _http.GetFromJsonAsync<List<UserModel>>("api/users");
        return users ?? new List<UserModel>();
    }

    public async Task<UserModel?> GetUser(int id)
    {
        return await _http.GetFromJsonAsync<UserModel>($"api/users/{id}");
    }

    public async Task CreateUser(UserModel user)
    {
        await _http.PostAsJsonAsync("api/users", user);
    }

    public async Task UpdateUser(UserModel user)
    {
        await _http.PutAsJsonAsync($"api/users/{user.UserId}", user);
    }

    public async Task DeleteUser(int id)
    {
        await _http.DeleteAsync($"api/users/{id}");
    }

    public async Task SetDefaultGarden(int userId, int gardenId)
    {
        await _http.PutAsync($"api/users/{userId}/default-garden/{gardenId}", null);
    }

    public async Task<int?> GetDefaultGarden(int userId)
    {
        return await _http.GetFromJsonAsync<int?>($"api/users/{userId}/default-garden");
    }


}
