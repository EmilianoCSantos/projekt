namespace Models
{
    public enum CardIssuer
    {
        Visa,
        MasterCard,
        Amex,
        Discover,
        Other
    }
    public interface ICreditCard
    {
        Guid CreditCardId { get; set; }
        CardIssuer Issuer { get; set; }
        string Number { get; set; }
        string ExpirationYear { get; set; }
        string ExpirationMonth { get; set; }
        string CardHolderName { get; set; }
        string EncryptedToken { get; set; }
        bool Seeded { get; set; }
    }
}