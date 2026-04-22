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



        // Location List Stays Visible when Navigating outside Garden Links
        public int? CurrentGardenId { get; private set; }

        public void SetGarden(int gardenId)
        {
            CurrentGardenId = gardenId;
            NotifyStateChanged();
        }




    }
}
