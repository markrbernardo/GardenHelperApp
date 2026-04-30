public class PlantWithInfoDto
{
    public int PlantId { get; set; }
    public string? Name { get; set; }

    public int PlantInformationId { get; set; }
    public string? ScientificName { get; set; }
    public string? CommonName { get; set; }

    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public int GardenId { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? GrowingSeason { get; set; }
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
    public string? Photo { get; set; }
    public string? PhotoMimeType { get; set; }

    public string? PlantPhoto { get; set; }
    public string? PlantPhotoMimeType { get; set; }

}
