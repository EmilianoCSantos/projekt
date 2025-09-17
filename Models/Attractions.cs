using Seido.Utilities.SeedGenerator;

namespace Models;


public class Attractions : IAttractions, ISeed<Attractions>
{
    public virtual Guid AttractionsId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LocationID { get; set; }
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Attractions Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AttractionsId = Guid.NewGuid();

        //Name = seeder.Name;
        //Description = seeder.Description; name och description måste adda i SeedGenerator

        return this;
    }
    #endregion
}