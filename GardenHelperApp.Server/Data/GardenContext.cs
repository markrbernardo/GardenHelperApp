using Microsoft.EntityFrameworkCore;
using GardenHelperApp.Shared.Models;

namespace GardenHelperApp.Server.Data;

public class GardenContext : DbContext
{
    public GardenContext(DbContextOptions<GardenContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<GardenModel> Gardens { get; set; }
    public DbSet<LocationModel> Locations { get; set; }
    public DbSet<PlantInformationModel> PlantInformation { get; set; }
    public DbSet<PlantModel> Plants { get; set; }
    public DbSet<ObservationModel> Observations { get; set; }
    public DbSet<JournalEntryModel> JournalEntries { get; set; }


}
