using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class PlantInformationModel
{
    [Key]
    public int PlantInformationId { get; set; }
    public string CommonName { get; set; }
    public string? ScientificName { get; set; }
}

