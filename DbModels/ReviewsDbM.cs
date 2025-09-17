using Seido.Utilities.SeedGenerator;
using Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace DbModels;
public class ReviewsDbM : Reviews, ISeed<ReviewsDbM>
{
    [Key]
    public override Guid ReviewsId { get; set; }

    public new ReviewsDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}