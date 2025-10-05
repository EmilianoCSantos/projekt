using Microsoft.Extensions.Logging;
using DbModels;
using DbRepos;

namespace Services;
    
public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;

    public Task SeedAsync(int nrItems) => _repo.SeedAsync(nrItems);
    public Task SeedUsersAsync(int nrItems) => _repo.SeedUsersAsync(nrItems);
    public Task SeedLocationsAsync(int nrItems) => _repo.SeedLocationsAsync(nrItems);
    public Task SeedAttractionsAsync(int nrItems) => _repo.SeedAttractionsAsync(nrItems);
    public Task SeedReviewsAsync(int nrItems) => _repo.SeedReviewsAsync(nrItems);
    public Task<List<AttractionsDbM>> GetFilteredAttractionsAsync(
        string category = null, 
        string title = null, 
        string description = null, 
        string country = null, 
        string city = null) => _repo.GetFilteredAttractionsAsync(category, title, description, country, city);
    

    #region constructors
    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }
    public AdminServiceDb(AdminDbRepos repo, ILogger<AdminServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

