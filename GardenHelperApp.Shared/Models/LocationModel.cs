using System.ComponentModel.DataAnnotations;
using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Shared.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }
        public int GardenId { get; set; }

        public string? Name { get; set; }

        // Outside vs inside stays as a bool
        public bool? IsOutside { get; set; }

        // ENUM — replaces string Lighting
        public LocationLighting Lighting { get; set; } = LocationLighting.Unknown;

        // These remain strings because they are descriptive, not categorical
        public string? WindowFacing { get; set; }
        public int? DistancetoLight { get; set; }
        public string? Humidity { get; set; }
        public string? TemperatureConsistency { get; set; }
        public string? Notes { get; set; }

        public string? Photo { get; set; }
    }
}
