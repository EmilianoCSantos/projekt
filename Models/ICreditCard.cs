namespace Models
{
    public interface ICreditCard
    {
        Guid CreditCardId { get; set; }
        CardIssues Issuer { get; set; }
        string Number { get; set; }
        string ExpirationYear { get; set; }
        string ExpirationMonth { get; set; }
        string CardHolderName { get; set; }
        string EncryptedToken { get; set; }
        bool Seeded { get; set; }
    }
}