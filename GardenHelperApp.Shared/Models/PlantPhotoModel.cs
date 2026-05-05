using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class PlantPhotoModel
{
    [Key]
    public int PhotoId { get; set; }

    public int PlantId { get; set; }

    public string? Photo { get; set; } = string.Empty;
    public string? PhotoMimeType { get; set; }


    // Match SQLite TEXT column exactly
    public DateTime CreatedAt { get; set; }
}
