using GardenHelperApp.Shared.Enums;

public class PlantWithInfoDto
{
    // ---------------------------------------------------------
    // PLANT (instance-specific)
    // ---------------------------------------------------------
    public int PlantId { get; set; }
    public string? Name { get; set; }
    public string DisplayName => Name ?? CommonName ?? ScientificName ?? "Unnamed Plant";

    public int GardenId { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Notes { get; set; }

    // ---------------------------------------------------------
    // PLANT INFORMATION (shared species data)
    // ---------------------------------------------------------
    public int PlantInformationId { get; set; }
    public string? ScientificName { get; set; }
    public string? CommonName { get; set; }

    public Season GrowingSeason { get; set; } = Season.Unknown;
    public string? Seeds { get; set; }

    public LightRequirement Light { get; set; } = LightRequirement.Unknown;
    public WaterRequirement Water { get; set; } = WaterRequirement.Unknown;
    public SoilType Soil { get; set; } = SoilType.Unknown;
    public ContainerType Container { get; set; } = ContainerType.Unknown;
    public FertilizerType Fertilization { get; set; } = FertilizerType.Unknown;
    public PropagationMethod Propagation { get; set; } = PropagationMethod.Unknown;
    public PlantHealthStatus Health { get; set; } = PlantHealthStatus.Unknown;

    // Free‑form fields that are not enum-worthy
    public string? Air { get; set; }
    public string? Pruning { get; set; }

    // Primary species photo
    public string? Photo { get; set; }
    public string? PhotoMimeType { get; set; }

    // ---------------------------------------------------------
    // PLANT INSTANCE PHOTO (optional)
    // ---------------------------------------------------------
    public string? PlantPhoto { get; set; }
    public string? PlantPhotoMimeType { get; set; }
}
