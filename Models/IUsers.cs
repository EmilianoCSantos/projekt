namespace Models;
public interface IUsers
{
    Guid UsersId { get; set; }
    string UserName { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EncryptedToken { get; set; } //AES encrypted version of the cc
    bool Seeded { get; set; }
}