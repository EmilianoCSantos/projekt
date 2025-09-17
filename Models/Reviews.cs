using Seido.Utilities.SeedGenerator;

namespace Models;


public class Reviews : IReviews, ISeed<Reviews>
{
    public virtual Guid ReviewsId { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public Guid UserId { get; set; }
    public Guid AttractionId { get; set; }
    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    #region Seeder
    public bool Seeded { get; set; } = false;

    public Reviews Seed(SeedGenerator seeder)
    {
        Seeded = true;
        ReviewsId = Guid.NewGuid();
     
        //Rating = seeder.Rating; dessa måste skapas i SeedGenerator
        //Comment = seeder.Comment;

        return this;
    }
    #endregion
}