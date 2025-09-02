# Exercise: Extending the GoodFriends Project with a Credit Card Model

This exercise will guide you through extending the current codebase (branch `8-extensions`) by adding a new Credit Card model and integrating it into the data access layer. Finally we will remove the Quote Models

## Prerequisites
- You have cloned the repository and checked out the `8-extensions` branch.
- You have a working .NET development environment.

---

## Steps

### 1. Create a New Feature Branch
Create a new branch from `8-extensions` for your work. Name it `8a-extensions-creditcard`

---

### 2. Add a Credit Card Model
Create interface `ICreditCard.cs` in the `Models/` folder.
Create a new model class `CreditCard.cs` in the `Models/` folder with the ISeed<> implemented. Example:

```csharp
public class CreditCard : ICreditCard, ISeed<CreditCard>
{
    public virtual Guid CreditCardId { get; set; }

    public CardIssues Issuer { get; set; }
    public string Number { get; set; }
    public string ExpirationYear { get; set; }
    public string ExpirationMonth { get; set; }    

    public string CardHolderName { get; set; }

    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    #region Seeder
    public bool Seeded { get; set; } = false;

    public CreditCard Seed (SeedGenerator seeder)
    {
        Seeded = true;
        CreditCardId = Guid.NewGuid();
        
        Issuer = seeder.FromEnum<CardIssues>();

        Number = $"{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}";
        ExpirationYear = $"{seeder.Next(25, 32)}";
        ExpirationMonth = $"{seeder.Next(01, 13):D2}";


        CardHolderName = seeder.FullName;
        return this;
    }
    #endregion
}
```
---

### 3. Add a Corresponding DbModel
Create a new class `CreditCardDbM.cs` in the `DbModels/` folder. The class shall inherit from CreditCard Model and override the EGC specific properties. 
Implement ISeed<> by simply calling the Seed method of the base class. Example:

```csharp
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
```

---


### 4. Seed the Credit Card DbModel
In `DbRepos/AdminDbRepos.cs`, update the `SeedAsync` method to seed 1000 `CreditCardDbM` instance. Example:

```csharp
// ...existing code...
        var creditcards = seeder.ItemsToList<CreditCardDbM>(1000);
        _dbContext.CreditCards.AddRange(creditcards);
// ...existing code...
```

---

### 5. Register the DbSet in DbContext
In `DbContext/MainDbContext.cs`, add a `DbSet<CreditCardDbM>` property:

```csharp
    public DbSet<CreditCardDbM> CreditCards { get; set; }
```

---

### 6. Build and Test
- Build the solution: `dotnet build`, ensure no errors
- Follow the instructions in readme-clr1.txt to build the databases and run the application and verify the new model is seeded.

---

### 7. Remove all template models
- remove Quote, IQuote, QuoteDbM
- remove QuoteDbM registration in DbContext/MainDbContext
- remove QuoteDbM Seeding in DbRepos/AdminDbRepos.SeedAsync.
- Build the solution: `dotnet build`, ensure no errors
- Follow the instructions in readme-clr1.txt to build the databases and run the application and verify the new model is seeded.

---

### 8. Make a new standalone solution from the branch
- Copy the entire solution folder with all files into a new folder named CreditCards
- Delete the .git folder in CreditCards
- Open the folder CreditCards in Visual Studio Code
- Delete the file GoodFriends.code-workspac
- Create a new Workspace using File->Save Workspace As...
- Rename file GoodFriends.sln to CreditCards.sln 
- Rename GoodFriends.sln to CreditCards.slnin in tasks.json
- Make an intial git commit
- Publish your repo to your gitub


---

## Deliverables
- A new standalone solution from the brancn with above changes
---
