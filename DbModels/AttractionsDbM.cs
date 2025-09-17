using Seido.Utilities.SeedGenerator;
using Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace DbModels;
public class AttractionsDbM : Attractions, ISeed<AttractionsDbM>
{
    [Key]
    public override Guid AttractionsId { get; set; }

    public new AttractionsDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}