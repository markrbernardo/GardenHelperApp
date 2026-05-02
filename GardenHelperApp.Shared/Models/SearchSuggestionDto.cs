

namespace GardenHelperApp.Shared.Models
{
    public class SearchSuggestionDto
    {
        public string Type { get; set; } = ""; // "plant", "plantInfo", "location", "garden"
        public int Id { get; set; }

        public string Primary { get; set; } = "";
        public string? Secondary { get; set; }
        public string? Tertiary { get; set; }

        public string? PhotoBase64 { get; set; }
        public string? PhotoMimeType { get; set; }
    }

}
