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

            // Hämta de faktiska sparade UserId från databasen
            _logger.LogInformation("Fetching saved UserIds from database...");
            var savedUserIds = await _dbContext.Users.Select(u => u.UsersId).ToListAsync();
            _logger.LogInformation($"Found {savedUserIds.Count} saved user IDs");

            // Hämta de faktiska sparade AttractionId från databasen
            _logger.LogInformation("Fetching saved AttractionIds from database...");
            var savedAttractionIds = await _dbContext.Attractions.Select(a => a.AttractionsId).ToListAsync();
            _logger.LogInformation($"Found {savedAttractionIds.Count} saved attraction IDs");

            // Seed Reviews table
            _logger.LogInformation("Seeding Reviews...");
            var reviews = seeder.ItemsToList<ReviewsDbM>(nrItems);
            _logger.LogInformation($"Created {reviews.Count} reviews");

            for (int i = 0; i < reviews.Count; i++)
            {
                // Tilldela varje review en giltig UserId och AttractionId från databasen
                reviews[i].UserId = savedUserIds[i % savedUserIds.Count];
                reviews[i].AttractionId = savedAttractionIds[i % savedAttractionIds.Count];
                _logger.LogInformation($"Review {i}: {reviews[i].Comment} -> UserId: {reviews[i].UserId}, AttractionId: {reviews[i].AttractionId}");
            }

            _logger.LogInformation("Adding Reviews to context...");
            _dbContext.Reviews.AddRange(reviews);

            _logger.LogInformation("Saving Reviews with FK relationships...");
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
    // Seeda 50 användare
    public async Task SeedUsersAsync(int nrItems)
    {
        try
        {
            var fn = Path.GetFullPath(_seedSource);
            var seeder = new SeedGenerator(fn);

            _logger.LogInformation("Seeding Users...");
            var users = seeder.ItemsToList<UsersDbM>(nrItems);
            _dbContext.Users.AddRange(users);

            _logger.LogInformation("Saving Users...");
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Users saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during seeding users: {ex.Message}");
            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }
    // Seeda 100 Locations
    public async Task SeedLocationsAsync(int nrItems)
    {
        try
        {
            var fn = Path.GetFullPath(_seedSource);
            var seeder = new SeedGenerator(fn);

            // Seed Locations table
            _logger.LogInformation("Seeding Locations...");
            var locations = seeder.ItemsToList<LocationsDbM>(nrItems);
            _dbContext.Locations.AddRange(locations);

            _logger.LogInformation("Saving Locations...");
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Locations saved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during seeding locations: {ex.Message}");
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
