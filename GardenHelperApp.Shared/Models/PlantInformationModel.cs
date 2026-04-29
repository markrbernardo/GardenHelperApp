using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class PlantInformationModel
{
    [Key]
    public int PlantInformationId { get; set; }

    public string CommonName { get; set; } = string.Empty;
    public string? ScientificName { get; set; }

    // New fields you added to SQLite
    public string? Description { get; set; }
    public string? GrowingSeason { get; set; }
    public string? Photo { get; set; }  // Base64 TEXT

    public string? Seeds { get; set; }
    public string? Light { get; set; }
    public string? Water { get; set; }
    public string? Air { get; set; }
    public string? Soil { get; set; }
    public string? Container { get; set; }
    public string? Fertilization { get; set; }
    public string? Pruning { get; set; }
    public string? Propagation { get; set; }
    public string? Health { get; set; }
    public string? PhotoMimeType { get; set; }

}
