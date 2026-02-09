using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GarMan.Data;
using GarMan;

Console.WriteLine("Starting database seeding...");

// Build configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName + "\\GarMan")
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: Connection string not found!");
    return;
}

// Create DbContext
var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(connectionString);

using var context = new ApplicationDbContext(optionsBuilder.Options);

// Ensure database is created
await context.Database.EnsureCreatedAsync();

// Run seeding (clear existing data and insert 50 samples)
await SeedAppliances.SeedSampleDataAsync(context, clearExisting: true);

Console.WriteLine("Seeding completed successfully!");
