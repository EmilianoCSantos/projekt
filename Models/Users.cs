using Seido.Utilities.SeedGenerator;

namespace Models;


public class Users : IUsers, ISeed<Users>
{
    public virtual Guid UsersId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Users Seed(SeedGenerator seeder)
    {
        Seeded = true;
        UsersId = Guid.NewGuid();

        //UserName = seeder.UserName;//måste adda UserName i SeedGenerator
        FirstName = seeder.FirstName;
        LastName = seeder.LastName;

        return this;
    }
    #endregion
}