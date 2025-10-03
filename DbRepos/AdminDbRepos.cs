using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(int nrItems)
{
    try
    {
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        // Seed Users table
        _logger.LogInformation("Seeding Users...");
        var users = seeder.ItemsToList<UsersDbM>(nrItems);
        _dbContext.Users.AddRange(users);

        // Seed Locations table
        _logger.LogInformation("Seeding Locations...");
        var locations = seeder.ItemsToList<LocationsDbM>(nrItems);
        _dbContext.Locations.AddRange(locations);

        _logger.LogInformation("Saving Users and Locations...");
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Users and Locations saved successfully");

        // Hämta de faktiska sparade LocationId från databasen
        _logger.LogInformation("Fetching saved LocationIds from database...");
        var savedLocationIds = await _dbContext.Locations.Select(l => l.LocationsId).ToListAsync();
        _logger.LogInformation($"Found {savedLocationIds.Count} saved location IDs");
        
        var attractions = seeder.ItemsToList<AttractionsDbM>(nrItems);
        _logger.LogInformation($"Created {attractions.Count} attractions");
        
        for (int i = 0; i < attractions.Count; i++)
        {
            // Tilldela varje attraction en giltig LocationId från databasen
            attractions[i].LocationId = savedLocationIds[i % savedLocationIds.Count];
            _logger.LogInformation($"Attraction {i}: {attractions[i].Name} -> LocationId: {attractions[i].LocationId}");
        }
        
        _logger.LogInformation("Adding Attractions to context...");
        _dbContext.Attractions.AddRange(attractions);
        
        _logger.LogInformation("Saving Attractions with FK relationships...");
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("All entities saved successfully with relationships!");
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error during seeding: {ex.Message}");
        if (ex.InnerException != null)
        {
            _logger.LogError($"Inner exception: {ex.InnerException.Message}");
        }
        throw;
    }
}

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
