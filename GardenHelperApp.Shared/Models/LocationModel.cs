using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class LocationModel
{
    [Key]
    public int LocationId { get; set; }
    public int GardenId { get; set; }
    public string? Name { get; set; }
    public string? Lighting { get; set; }
    public bool? IsOutside { get; set; }
}
