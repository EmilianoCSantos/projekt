namespace Services;

public interface IAdminService
{
    public Task SeedAsync(int nrItems);
    public Task SeedUsersAsync(int nrItems);
    public Task SeedLocationsAsync(int nrItems);
    public Task SeedAttractionsAsync(int nrItems);
}
