namespace Models;
public interface IReviews
{
    Guid ReviewsId { get; set; }
    string Comment { get; set; }
    int Rating { get; set; }
    Guid UserId { get; set; }
    Guid AttractionId { get; set; }
    string EncryptedToken { get; set; } //AES encrypted version of the cc
    bool Seeded { get; set; }
}