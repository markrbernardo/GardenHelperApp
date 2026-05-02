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

        // GARDEN → LOCATIONS
        modelBuilder.Entity<LocationModel>()
            .HasOne<GardenModel>()              // no navigation property
            .WithMany()                         // no navigation property
            .HasForeignKey(l => l.GardenId)
            .OnDelete(DeleteBehavior.Cascade);

        // GARDEN → PLANTS
        modelBuilder.Entity<PlantModel>()
            .HasOne<GardenModel>()
            .WithMany()
            .HasForeignKey(p => p.GardenId)
            .OnDelete(DeleteBehavior.Cascade);

        // LOCATION → PLANTS
        modelBuilder.Entity<PlantModel>()
            .HasOne<LocationModel>()
            .WithMany()
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        // PLANT → OBSERVATIONS
        modelBuilder.Entity<ObservationModel>()
            .HasOne<PlantModel>()
            .WithMany()
            .HasForeignKey(o => o.PlantId)
            .OnDelete(DeleteBehavior.Cascade);

        // GARDEN → JOURNAL ENTRIES
        modelBuilder.Entity<JournalEntryModel>()
            .HasOne<GardenModel>()
            .WithMany()
            .HasForeignKey(j => j.GardenId)
            .OnDelete(DeleteBehavior.Cascade);

        // Timestamp fields
        modelBuilder.Entity<ObservationModel>()
            .Property(o => o.CreatedAt)
            .HasColumnType("TEXT");

        modelBuilder.Entity<ObservationModel>()
            .Property(o => o.UpdatedAt)
            .HasColumnType("TEXT");
    }


}
