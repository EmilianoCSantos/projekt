using Seido.Utilities.SeedGenerator;
using Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace DbModels;
public class LocationsDbM : Locations, ISeed<LocationsDbM>
{
    [Key]
    public override Guid LocationsId { get; set; }

    public new LocationsDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}