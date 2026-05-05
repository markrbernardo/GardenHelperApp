using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Shared.Models
{
    public class SearchSuggestionDto
    {
        // ---------------------------------------------------------
        // TYPE OF RESULT
        // ---------------------------------------------------------
        public SuggestionType Type { get; set; } = SuggestionType.Unknown;

        // The ID of the entity represented by this suggestion
        public int Id { get; set; }

        // ---------------------------------------------------------
        // DISPLAY TEXT
        // ---------------------------------------------------------
        public string Primary { get; set; } = string.Empty;
        public string? Secondary { get; set; }
        public string? Tertiary { get; set; }

        // ---------------------------------------------------------
        // PHOTO (optional)
        // ---------------------------------------------------------
        public string? PhotoBase64 { get; set; }
        public string? PhotoMimeType { get; set; }
    }
}
