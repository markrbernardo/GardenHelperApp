using Microsoft.JSInterop;

public class UserSessionService
{
    private readonly IJSRuntime _js;

    // ---------------------------------------------------------
    // SESSION STATE
    // ---------------------------------------------------------
    public int? CurrentUserId { get; private set; }
    public string? CurrentUserName { get; private set; }
    public int? CurrentGardenId { get; private set; }
    public int? DefaultGardenId { get; private set; }
    public int? CurrentLocationId { get; private set; }
    public int? CurrentPlantId { get; private set; }

    public event Func<Task>? OnChange;

    public UserSessionService(IJSRuntime js)
    {
        _js = js;
    }

    // ---------------------------------------------------------
    // INITIALIZATION
    // ---------------------------------------------------------
    public async Task InitializeAsync()
    {
        CurrentUserId = await GetIntAsync("userId");
        CurrentUserName = await GetStringAsync("userName");
        CurrentGardenId = await GetIntAsync("gardenId");
        DefaultGardenId = await GetIntAsync("defaultGardenId");
        CurrentLocationId = await GetIntAsync("locationId");
        CurrentPlantId = await GetIntAsync("plantId");

        await NotifyStateChangedAsync();
    }

    // ---------------------------------------------------------
    // USER
    // ---------------------------------------------------------
    public async Task SetUserAsync(int userId, string userName)
    {
        CurrentUserId = userId;
        CurrentUserName = userName;

        await SetIntAsync("userId", userId);
        await SetStringAsync("userName", userName);

        await NotifyStateChangedAsync();
    }

    // ---------------------------------------------------------
    // GARDEN
    // ---------------------------------------------------------
    public async Task SetGardenAsync(int gardenId)
    {
        CurrentGardenId = gardenId;
        await SetIntAsync("gardenId", gardenId);
        await NotifyStateChangedAsync();
    }

    public async Task SetDefaultGardenAsync(int gardenId)
    {
        DefaultGardenId = gardenId;
        await SetIntAsync("defaultGardenId", gardenId);
        await NotifyStateChangedAsync();
    }

    public async Task ClearGardenIfDeletedAsync(int deletedGardenId)
    {
        if (CurrentGardenId == deletedGardenId)
        {
            CurrentGardenId = null;
            await RemoveAsync("gardenId");
            await NotifyStateChangedAsync();
        }
    }

    public async Task ClearCurrentGardenAsync()
    {
        CurrentGardenId = null;
        await RemoveAsync("gardenId");
        await NotifyStateChangedAsync();
    }

    // ---------------------------------------------------------
    // LOCATION / PLANT CONTEXT
    // ---------------------------------------------------------
    public async Task SetCurrentLocationAsync(int? locationId, bool persist = false)
    {
        CurrentLocationId = locationId;

        if (persist)
        {
            if (locationId.HasValue)
                await SetIntAsync("locationId", locationId.Value);
            else
                await RemoveAsync("locationId");
        }

        await NotifyStateChangedAsync();
    }

    public async Task SetCurrentPlantAsync(int? plantId, bool persist = false)
    {
        CurrentPlantId = plantId;

        if (persist)
        {
            if (plantId.HasValue)
                await SetIntAsync("plantId", plantId.Value);
            else
                await RemoveAsync("plantId");
        }

        await NotifyStateChangedAsync();
    }

    // ---------------------------------------------------------
    // CLEAR SESSION
    // ---------------------------------------------------------
    public async Task ClearAsync()
    {
        CurrentUserId = null;
        CurrentUserName = null;
        CurrentGardenId = null;
        DefaultGardenId = null;
        CurrentLocationId = null;
        CurrentPlantId = null;

        await RemoveAsync("userId");
        await RemoveAsync("userName");
        await RemoveAsync("gardenId");
        await RemoveAsync("defaultGardenId");
        await RemoveAsync("locationId");
        await RemoveAsync("plantId");

        await NotifyStateChangedAsync();
    }

    // ---------------------------------------------------------
    // INTERNAL HELPERS
    // ---------------------------------------------------------
    private async Task<int?> GetIntAsync(string key)
    {
        var value = await _js.InvokeAsync<string?>("localStorage.getItem", key);
        return int.TryParse(value, out var result) ? result : null;
    }

    private Task<string?> GetStringAsync(string key) =>
        _js.InvokeAsync<string?>("localStorage.getItem", key).AsTask();

    private Task SetIntAsync(string key, int value) =>
        _js.InvokeVoidAsync("localStorage.setItem", key, value).AsTask();

    private Task SetStringAsync(string key, string value) =>
        _js.InvokeVoidAsync("localStorage.setItem", key, value).AsTask();

    private Task RemoveAsync(string key) =>
        _js.InvokeVoidAsync("localStorage.removeItem", key).AsTask();

    private async Task NotifyStateChangedAsync()
    {
        if (OnChange != null)
            await OnChange.Invoke();
    }
}
