namespace Models;
public interface ILocations
{
    Guid LocationsId { get; set; }
    string Country { get; set; }
    string City { get; set; }
    string EncryptedToken { get; set; } //AES encrypted version of the cc
    bool Seeded { get; set; }
}
