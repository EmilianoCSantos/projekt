using Microsoft.Extensions.Logging;

using DbRepos;

namespace Services;
    
public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;

    public Task SeedAsync(int nrItems) => _repo.SeedAsync(nrItems);
    public Task SeedUsersAsync(int nrItems) => _repo.SeedUsersAsync(nrItems);
    public Task SeedLocationsAsync(int nrItems) => _repo.SeedLocationsAsync(nrItems);

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

