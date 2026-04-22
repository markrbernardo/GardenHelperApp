using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models
{
    public class GardenModel
    {
        [Key]
        public int GardenId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int? ZipCode { get; set; }
    }
}

