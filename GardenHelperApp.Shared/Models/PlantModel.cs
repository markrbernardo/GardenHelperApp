using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class PlantModel
{
    [Key]
    public int PlantId { get; set; }
    public int LocationId { get; set; }
    public int GardenId { get; set; }
    public int PlantInformationId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}
