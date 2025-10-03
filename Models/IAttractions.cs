namespace Models;
public interface IAttractions
{
    Guid AttractionsId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Guid? LocationId { get; set; } 
    string EncryptedToken { get; set; } //AES encrypted version of the cc
    bool Seeded { get; set; }
}