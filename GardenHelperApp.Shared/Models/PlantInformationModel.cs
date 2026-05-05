using System.ComponentModel.DataAnnotations;
using GardenHelperApp.Shared.Enums;

namespace GardenHelperApp.Shared.Models;

public class PlantInformationModel
{
    [Key]
    public int PlantInformationId { get; set; }

    public string CommonName { get; set; } = string.Empty;
    public string? ScientificName { get; set; }

    // Description + photo
    public string? Description { get; set; }
    public string? Photo { get; set; }  // Base64
    public string? PhotoMimeType { get; set; }

    // Seeds remain free‑form text
    public string? Seeds { get; set; }

    // ENUM FIELDS
    public Season GrowingSeason { get; set; } = Season.Unknown;
    public LightRequirement Light { get; set; } = LightRequirement.Unknown;
    public WaterRequirement Water { get; set; } = WaterRequirement.Unknown;
    public SoilType Soil { get; set; } = SoilType.Unknown;
    public ContainerType Container { get; set; } = ContainerType.Unknown;
    public FertilizerType Fertilization { get; set; } = FertilizerType.Unknown;
    public PropagationMethod Propagation { get; set; } = PropagationMethod.Unknown;
    public PlantHealthStatus Health { get; set; } = PlantHealthStatus.Unknown;

    // Air remains free‑form text (not enum-worthy)
    public string? Air { get; set; }

    // Pruning remains free‑form text (not enum-worthy)
    public string? Pruning { get; set; }
}
