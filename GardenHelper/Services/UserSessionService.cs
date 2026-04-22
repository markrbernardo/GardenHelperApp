using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Client.Services
{
    public class UserSessionService
    {
        public event Action? OnChange;

        public int? CurrentUserId { get; private set; }
        public string? CurrentUserName { get; private set; }

        public void SetUser(int userId, string userName)
        {
            CurrentUserId = userId;
            CurrentUserName = userName;
            NotifyStateChanged();
        }

        public void Clear()
        {
            CurrentUserId = null;
            CurrentUserName = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
