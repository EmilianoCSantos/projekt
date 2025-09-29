using Seido.Utilities.SeedGenerator;

namespace Models;


public class Users : IUsers, ISeed<Users>
{
    public virtual Guid UsersId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    // Navigation Properties
    public virtual ICollection<Reviews> Reviews { get; set; } = new List<Reviews>();

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Users Seed(SeedGenerator seeder)
    {
        Seeded = true;
        UsersId = Guid.NewGuid();

        UserName = seeder.PetName;//använder PetName som UserName i SeedGenerator
        FirstName = seeder.FirstName;
        LastName = seeder.LastName;

        return this;
    }
    #endregion
}