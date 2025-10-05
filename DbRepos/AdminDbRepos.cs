using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using System.Runtime.CompilerServices;

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
    // Seeda 1000 Attractions med FK till Locations
    public async Task SeedAttractionsAsync(int nrItems)
    {
        try
        {
            var fn = Path.GetFullPath(_seedSource);
            var seeder = new SeedGenerator(fn);

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
            _logger.LogError($"Error during seeding attractions: {ex.Message}");
            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }

    //Seed Reviews 0 till 20 per Attraction
    public async Task SeedReviewsAsync(int nrItems)
    {
        try
        {
            var fn = Path.GetFullPath(_seedSource);
            var seeder = new SeedGenerator(fn);

            // Hämta de faktiska sparade UserId från databasen
            _logger.LogInformation("Fetching saved UserIds from database...");
            var savedUserIds = await _dbContext.Users.Select(u => u.UsersId).ToListAsync();
            _logger.LogInformation($"Found {savedUserIds.Count} saved user IDs");

            // Hämta de faktiska sparade AttractionId från databasen
            _logger.LogInformation("Fetching saved AttractionIds from database...");
            var savedAttractionIds = await _dbContext.Attractions.Select(a => a.AttractionsId).ToListAsync();
            _logger.LogInformation($"Found {savedAttractionIds.Count} saved attraction IDs");

            // Skapa reviews för varje attraction (0-20 reviews per attraction)
            _logger.LogInformation("Creating reviews for each attraction (0-20 per attraction)...");
            var allReviews = new List<ReviewsDbM>();

            // Loop genom alla attractions
            foreach (var attractionId in savedAttractionIds)
            {
                // Slumpmässigt antal reviews för denna attraction (0-20)
                int randomReviewCount = seeder.Next(0, 21); // 0-20 inklusive
                _logger.LogInformation($"Creating {randomReviewCount} reviews for attraction {attractionId}");

                // Skapa det antalet reviews för denna attraction
                for (int i = 0; i < randomReviewCount; i++)
                {
                    var review = new ReviewsDbM().Seed(seeder);
                    review.AttractionId = attractionId;
                    review.UserId = savedUserIds[seeder.Next(0, savedUserIds.Count)]; // Random user
                    allReviews.Add(review);
                }
            }

            _logger.LogInformation($"Created total of {allReviews.Count} reviews for all attractions");
            _logger.LogInformation("Adding Reviews to context...");
            _dbContext.Reviews.AddRange(allReviews);

            _logger.LogInformation("Saving Reviews with FK relationships...");
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("All entities saved successfully with relationships!");

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during seeding reviews: {ex.Message}");
            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }

    //Visa alla sevärdheter filtrerade på kategori, rubrik, beskrivning, land och ort
    public async Task<List<AttractionsDbM>> GetFilteredAttractionsAsync(
        string category = null, 
        string title = null, 
        string description = null, 
        string country = null, 
        string city = null)
    {
        try
        {
            _logger.LogInformation("Filtering attractions with parameters: category={category}, title={title}, description={description}, country={country}, city={city}",
                category, title, description, country, city);

            // Starta med grundläggande query och inkludera Location
            var query = _dbContext.Attractions
                .Include(a => a.Location)
                .AsQueryable();

            // Filtrera på kategori (söker i Name-fältet där PlaceType finns)
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Name.Contains(category));
                _logger.LogInformation("Applied category filter: {category}", category);
            }

            // Filtrera på titel/namn
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(a => a.Name.Contains(title));
                _logger.LogInformation("Applied title filter: {title}", title);
            }

            // Filtrera på beskrivning
            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(a => a.Description.Contains(description));
                _logger.LogInformation("Applied description filter: {description}", description);
            }

            // Filtrera på land
            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(a => a.Location.Country.Contains(country));
                _logger.LogInformation("Applied country filter: {country}", country);
            }

            // Filtrera på stad
            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(a => a.Location.City.Contains(city));
                _logger.LogInformation("Applied city filter: {city}", city);
            }

            var result = await query.ToListAsync();
            _logger.LogInformation("Found {count} attractions matching filters", result.Count);
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error filtering attractions");
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
