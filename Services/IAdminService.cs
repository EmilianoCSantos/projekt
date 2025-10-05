using DbModels;
using Models;  // För att hitta AttractionsWithCommentsDto

namespace Services;

public interface IAdminService
{
    public Task SeedAsync(int nrItems);
    public Task SeedUsersAsync(int nrItems);
    public Task SeedLocationsAsync(int nrItems);
    public Task SeedAttractionsAsync(int nrItems);
    public Task SeedReviewsAsync(int nrItems);
    public Task<List<AttractionsDbM>> GetFilteredAttractionsAsync(
        string category = null,
        string title = null,
        string description = null,
        string country = null,
        string city = null);

    public Task<List<AttractionsDbM>> GetAttractionsWithoutReviewsAsync();

    public Task<AttractionWithCommentsDto> GetAttractionWithCommentsAsync(Guid attractionId);
}
