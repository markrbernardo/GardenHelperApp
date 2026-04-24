using Microsoft.JSInterop;

public class UserSessionService
{
    private readonly IJSRuntime _js;

    public int? CurrentUserId { get; private set; }
    public string? CurrentUserName { get; private set; }
    public int? CurrentGardenId { get; private set; }
    public int? DefaultGardenId { get; private set; }


    public event Func<Task>? OnChange;

    public UserSessionService(IJSRuntime js)
    {
        _js = js;
    }

    // Called once at startup in Program.cs
    public async Task InitializeAsync()
    {
        var userIdString = await _js.InvokeAsync<string?>("localStorage.getItem", "userId");
        var userNameString = await _js.InvokeAsync<string?>("localStorage.getItem", "userName");
        var gardenIdString = await _js.InvokeAsync<string?>("localStorage.getItem", "gardenId");
        var defaultGardenString = await _js.InvokeAsync<string?>("localStorage.getItem", "defaultGardenId");

        DefaultGardenId = int.TryParse(defaultGardenString, out var dg) ? dg : null;
        CurrentUserId = int.TryParse(userIdString, out var uid) ? uid : null;
        CurrentUserName = userNameString;
        CurrentGardenId = int.TryParse(gardenIdString, out var gid) ? gid : null;

        await NotifyStateChanged();
    }


    // Set the logged-in user
    public async Task SetUser(int userId, string userName)
    {
        CurrentUserId = userId;
        CurrentUserName = userName;

        await _js.InvokeVoidAsync("localStorage.setItem", "userId", userId);
        await _js.InvokeVoidAsync("localStorage.setItem", "userName", userName);

        await NotifyStateChanged();
    }

    // Set the selected garden
    public async Task SetGarden(int gardenId)
    {
        CurrentGardenId = gardenId;

        await _js.InvokeVoidAsync("localStorage.setItem", "gardenId", gardenId);

        await NotifyStateChanged();
    }

    // Clear everything on sign-out
    public async Task Clear()
    {
        CurrentUserId = null;
        CurrentUserName = null;
        CurrentGardenId = null;
        DefaultGardenId = null;

        await _js.InvokeVoidAsync("localStorage.removeItem", "userId");
        await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
        await _js.InvokeVoidAsync("localStorage.removeItem", "gardenId");
        await _js.InvokeVoidAsync("localStorage.removeItem", "defaultGardenId");
        

        await NotifyStateChanged();
    }

    private async Task NotifyStateChanged()
    {
        if (OnChange != null)
            await OnChange.Invoke();
    }

    public async Task SetDefaultGarden(int gardenId)
    {
        DefaultGardenId = gardenId;
        await _js.InvokeVoidAsync("localStorage.setItem", "defaultGardenId", gardenId);
        await NotifyStateChanged();
    }

}
