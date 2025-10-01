using Seido.Utilities.SeedGenerator;

namespace Models;


public class Locations : ILocations, ISeed<Locations>
{
    public virtual Guid LocationsId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    // Navigation Properties
    public virtual ICollection<Attractions> Attractions { get; set; } = new List<Attractions>();

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Locations Seed(SeedGenerator seeder)
    {
        Seeded = true;
        LocationsId = Guid.NewGuid();

        Country = seeder.Country;
        City = seeder.City();

        return this;
    }
    #endregion
}