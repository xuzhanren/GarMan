using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GarMan.Models;

namespace GarMan.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Appliance> Appliances { get; set; }
    public DbSet<RecyclingCenter> RecyclingCenters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for Weight property
        modelBuilder.Entity<Appliance>()
            .Property(a => a.Weight)
            .HasPrecision(18, 2);

        // Configure decimal precision for EstimatedValue property
        modelBuilder.Entity<Appliance>()
            .Property(a => a.EstimatedValue)
            .HasPrecision(18, 2);

        // Configure decimal precision for RecycledValue property
        modelBuilder.Entity<Appliance>()
            .Property(a => a.RecycledValue)
            .HasPrecision(18, 2);

        // Seed some initial recycling centers
        modelBuilder.Entity<RecyclingCenter>().HasData(
            new RecyclingCenter
            {
                Id = 1,
                CenterName = "Green Tech Recycling",
                Address = "123 Eco Street",
                City = "Springfield",
                State = "IL",
                ZipCode = "62701",
                PhoneNumber = "555-0100",
                Email = "info@greentech.com",
                OperatingHours = "Mon-Fri 8AM-5PM",
                AcceptedAppliances = "Refrigerators, Washers, Dryers, Dishwashers",
                IsActive = true
            },
            new RecyclingCenter
            {
                Id = 2,
                CenterName = "Eco Appliance Solutions",
                Address = "456 Recycle Avenue",
                City = "Springfield",
                State = "IL",
                ZipCode = "62702",
                PhoneNumber = "555-0200",
                Email = "contact@ecoapp.com",
                OperatingHours = "Mon-Sat 9AM-6PM",
                AcceptedAppliances = "All major appliances, Small electronics",
                IsActive = true
            }
        );
    }
}
