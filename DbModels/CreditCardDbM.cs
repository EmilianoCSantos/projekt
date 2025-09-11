using Seido.Utilities.SeedGenerator;
using Models;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace DbModels;
public class CreditCardDbM : CreditCard, ISeed<CreditCardDbM>
{
    [Key]
    public override Guid CreditCardId { get; set; }

    public new CreditCardDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}