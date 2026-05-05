using System.ComponentModel.DataAnnotations;

namespace GardenHelperApp.Shared.Models;

public class UserModel
{
    [Key]
    public int UserId { get; set; }
    public string? Name { get; set; } = string.Empty;

    public int? DefaultGardenId { get; set; }
}
