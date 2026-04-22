using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models
{
    public class JournalEntryModel
    {
        [Key]
        public int JournalEntryId { get; set; }
        public int UserId { get; set; }
        public int GardenId { get; set; }

        public string? Entry {  get; set; }

    }
}
