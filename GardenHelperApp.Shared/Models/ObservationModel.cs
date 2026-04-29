using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models
{
    public class ObservationModel
    {
        [Key]
        public int ObservationId { get; set; }

        public int UserId { get; set; }
        public int PlantId { get; set; }

        // Use DateTimeOffset for robust time handling
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool ActiveObservation { get; set; }
        public string? Observation { get; set; }
    }
}
