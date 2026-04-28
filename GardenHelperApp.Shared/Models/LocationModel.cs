using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public int GardenId { get; set; }

        public string? Name { get; set; }
        public bool? IsOutside { get; set; }
        public string? Lighting { get; set; }
        public string? WindowFacing { get; set; }
        public int? DistancetoLight { get; set; }
        public string? Humidity { get; set; }
        public string? TemperatureConsistency { get; set; }
        public string? Notes { get; set; }
        public string? Photo { get; set; }
    }
}
