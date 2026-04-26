using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models
{
    public class ObservationModel
    {
        [Key]
        public int ObservationId { get; set; }
        public int PlantId { get; set; }
        public string? Observation { get; set; }
        public bool ActiveObservation { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
