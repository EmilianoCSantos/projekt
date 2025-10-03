using Seido.Utilities.SeedGenerator;

namespace Models;


public class Attractions : IAttractions, ISeed<Attractions>
{
    public virtual Guid AttractionsId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid? LocationId { get; set; } 
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    // Navigation Properties
    public virtual Locations Location { get; set; }
    //public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Attractions Seed(SeedGenerator seeder)
    {
        Seeded = true;
        AttractionsId = Guid.NewGuid();

        Name = $"{seeder.LastName} {seeder.PlaceType}";
        Description = seeder.LatinSentence;

        return this;
    }
    #endregion
}