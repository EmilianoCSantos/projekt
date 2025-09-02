# Exercise: Extending the GoodFriends Project with a Credit Card Model

This exercise will guide you through extending the current codebase (branch `8-extensions`) by adding a new Credit Card model and integrating it into the data access layer. Finally we will remove the Quote Models

## Prerequisites
- You have cloned the repository and checked out the `8-extensions` branch.
- You have a working .NET development environment.

---

## Steps

### 1. Create a New Feature Branch
Spawn a new branch from `8-extensions` for your work:

---

### 2. Add a Credit Card Model
Create a new model class `CreditCard.cs` in the `Models/` folder. Example properties:

```csharp
public class CreditCard
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public string CardHolder { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Cvv { get; set; }
}
```

---

### 3. Add a Corresponding DbModel
Create a new class `CreditCardDbM.cs` in the `DbModels/` folder. The class shall inherit from CreditCard Model Example:

```csharp
public class CreditCardDbM : CreditCard
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public string CardHolder { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string Cvv { get; set; }
}
```

---



### 4. Seed the Credit Card DbModel
In `DbRepos/AdminDbRepos.cs`, update the `SeedAsync` method to seed at least one `CreditCardDbM` instance. Example:

```csharp
// ...existing code...
context.CreditCards.Add(new CreditCardDbM {
    CardNumber = "4111111111111111",
    CardHolder = "Test User",
    ExpiryDate = new DateTime(2030, 12, 31),
    Cvv = "123"
});
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
- Build the solution: `dotnet build`
- Run the application and verify the new model is seeded.

---

## Deliverables
- A new branch with the above changes.
- Code for the model, DbModel, seeding, and DbSet registration.

---

**Tip:** Follow best practices for naming and code organization. Commit your changes with clear messages.
