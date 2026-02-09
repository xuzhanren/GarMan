using GarMan.Data;
using GarMan.Models;
using Microsoft.EntityFrameworkCore;

namespace GarMan;

public class SeedAppliances
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context, bool clearExisting = false)
    {
        // Clear existing data if requested
        if (clearExisting && await context.Appliances.AnyAsync())
        {
            Console.WriteLine("Clearing existing appliance data...");
            context.Appliances.RemoveRange(context.Appliances);
            await context.SaveChangesAsync();
            Console.WriteLine("Existing data cleared.");
        }

        // Check if we already have appliance data
        if (await context.Appliances.AnyAsync())
        {
            Console.WriteLine("Database already contains appliances. Skipping seed.");
            return;
        }

        Console.WriteLine("Seeding 50 sample appliances...");

        var random = new Random();
        var users = new[] { "User1", "User2", "User3", "User4", "User5" };

        var applianceTypes = new[]
        {
            "Refrigerator", "Washing Machine", "Dryer", "Dishwasher", "Microwave",
            "Oven", "Stove", "Air Conditioner", "Freezer", "Water Heater",
            "Television", "Vacuum Cleaner", "Toaster", "Coffee Maker", "Blender"
        };

        var brands = new[]
        {
            "Samsung", "LG", "Whirlpool", "GE", "Maytag",
            "Bosch", "Kenmore", "Frigidaire", "KitchenAid", "Panasonic"
        };

        var addresses = new[]
        {
            "123 Main St, Springfield, IL 62701",
            "456 Oak Ave, Chicago, IL 60601",
            "789 Elm St, Naperville, IL 60540",
            "321 Pine Rd, Aurora, IL 60505",
            "654 Maple Dr, Joliet, IL 60435",
            "987 Cedar Ln, Rockford, IL 61101",
            "147 Birch Ct, Peoria, IL 61602",
            "258 Walnut Way, Champaign, IL 61820",
            "369 Ash Blvd, Bloomington, IL 61701",
            "741 Cherry St, Decatur, IL 62521"
        };

        var customerNames = new[]
        {
            "John Smith", "Mary Johnson", "Robert Williams", "Patricia Brown", "Michael Davis",
            "Linda Miller", "David Wilson", "Elizabeth Moore", "James Taylor", "Jennifer Anderson",
            "William Thomas", "Barbara Jackson", "Richard White", "Susan Harris", "Joseph Martin"
        };

        var appliances = new List<Appliance>();

        for (int i = 0; i < 50; i++)
        {
            var applianceType = applianceTypes[random.Next(applianceTypes.Length)];
            var brand = brands[random.Next(brands.Length)];
            var userId = users[random.Next(users.Length)];
            var customerName = customerNames[random.Next(customerNames.Length)];
            var address = addresses[random.Next(addresses.Length)];

            var appliance = new Appliance
            {
                ApplianceType = applianceType,
                Brand = brand,
                ModelNumber = $"{brand.Substring(0, 2).ToUpper()}{random.Next(1000, 9999)}",
                YearOfManufacture = random.Next(2005, 2024),
                Condition = (ApplianceCondition)random.Next(0, 4),
                Description = $"{applianceType} in {Enum.GetName(typeof(ApplianceCondition), random.Next(0, 4))} condition",
                Weight = Math.Round((decimal)(random.NextDouble() * 100 + 10), 2),
                CollectionDate = DateTime.Now.AddDays(random.Next(-30, 60)),
                CollectionAddress = address,
                CollectionFee = random.Next(30, 100),
                Status = (CollectionStatus)random.Next(0, 5),
                RecyclingStatus = (RecyclingStatus)random.Next(0, 5),
                EstimatedValue = random.Next(0, 2) == 0 ? Math.Round((decimal)(random.NextDouble() * 500 + 50), 2) : null,
                RecycledValue = random.Next(0, 3) == 0 ? Math.Round((decimal)(random.NextDouble() * 400 + 30), 2) : null,
                RecyclingComment = random.Next(0, 3) == 0 ? "Processed successfully" : null,
                DateSubmitted = DateTime.Now.AddDays(-random.Next(1, 90)),
                LastUpdated = DateTime.Now.AddDays(-random.Next(0, 30)),
                UserId = userId,
                CustomerName = customerName,
                CustomerPhone = $"555-{random.Next(100, 999)}-{random.Next(1000, 9999)}",
                CustomerEmail = $"{customerName.Replace(" ", ".").ToLower()}@example.com"
            };

            appliances.Add(appliance);
        }

        await context.Appliances.AddRangeAsync(appliances);
        await context.SaveChangesAsync();

        Console.WriteLine($"Successfully seeded {appliances.Count} sample appliances!");
    }
}
