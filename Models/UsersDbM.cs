using Seido.Utilities.SeedGenerator;
using Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace DbModels;
public class UsersDbM : Users, ISeed<UsersDbM>
{
    [Key]
    public override Guid UsersId { get; set; }

    public new UsersDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}