using System.Net.Http.Json;
using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Client.Services
{
    public class JournalEntryService
    {
        private readonly HttpClient _http;

        public JournalEntryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<JournalEntryModel>> GetAll()
        {
            return await _http.GetFromJsonAsync<List<JournalEntryModel>>("api/journalentries")
                   ?? new List<JournalEntryModel>();
        }

        public async Task<JournalEntryModel?> Get(int id)
        {
            return await _http.GetFromJsonAsync<JournalEntryModel>($"api/journalentries/{id}");
        }

        public async Task<List<JournalEntryModel>> GetByUser(int userId)
        {
            return await _http.GetFromJsonAsync<List<JournalEntryModel>>($"api/journalentries/user/{userId}")
                   ?? new List<JournalEntryModel>();
        }

        public async Task<List<JournalEntryModel>> GetByGarden(int gardenId)
        {
            return await _http.GetFromJsonAsync<List<JournalEntryModel>>($"api/journalentries/garden/{gardenId}")
                   ?? new List<JournalEntryModel>();
        }

        public async Task Create(JournalEntryModel model)
        {
            await _http.PostAsJsonAsync("api/journalentries", model);
        }

        public async Task Update(JournalEntryModel model)
        {
            await _http.PutAsJsonAsync($"api/journalentries/{model.JournalEntryId}", model);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"api/journalentries/{id}");
        }

    }
}
