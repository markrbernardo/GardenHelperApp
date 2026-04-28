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

    // ADD: PlantPhotos DbSet so EF knows about the table
    public DbSet<PlantPhotoModel> PlantPhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // When a Garden is deleted → delete its Locations
        modelBuilder.Entity<LocationModel>()
            .HasOne<GardenModel>()
            .WithMany()
            .HasForeignKey(l => l.GardenId)
            .OnDelete(DeleteBehavior.Cascade);

        // When a Location is deleted → delete its Plants
        modelBuilder.Entity<PlantModel>()
            .HasOne<LocationModel>()
            .WithMany()
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        // OPTIONAL: When a Garden is deleted → delete its Plants directly
        modelBuilder.Entity<PlantModel>()
            .HasOne<GardenModel>()
            .WithMany()
            .HasForeignKey(p => p.GardenId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ObservationModel>()
            .Property(o => o.CreatedAt)
            .HasColumnType("TEXT");

        modelBuilder.Entity<ObservationModel>()
            .Property(o => o.UpdatedAt)
            .HasColumnType("TEXT");

        // Ensure PlantPhotos.CreatedAt is stored as TEXT as well
        modelBuilder.Entity<PlantPhotoModel>()
            .Property(p => p.CreatedAt)
            .HasColumnType("TEXT");
    }
}
