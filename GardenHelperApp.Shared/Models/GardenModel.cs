using System.ComponentModel.DataAnnotations;
using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Shared.Models
{
    public class GardenModel
    {
        [Key]
        public int GardenId { get; set; }
        public int UserId { get; set; }

        public string? Name { get; set; }
        public string? ZipCode { get; set; }

        // Keep these as strings — too many possible values
        public string? HardinessZone { get; set; }
        public string? HeatZone { get; set; }

        // ENUMS
        public WindOrientation WindOrientation { get; set; } = WindOrientation.Unknown;
        public SoilType SoilType { get; set; } = SoilType.Unknown;
        public MarineInfluence MarineInfluence { get; set; } = MarineInfluence.Unknown;
    }
}
