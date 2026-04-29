public class PlantWithInfoDto
{
    public int PlantId { get; set; }
    public string? Name { get; set; }

    public int PlantInformationId { get; set; }
    public string? ScientificName { get; set; }
    public string? CommonName { get; set; }

    public int LocationId { get; set; }
    public int GardenId { get; set; }

    public string? Description { get; set; }
    public string? Notes { get; set; }
}
