namespace ProductCampaignManager.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public decimal EmeraldBalance { get; private set; }

    public ICollection<Campaign> Campaigns { get; private set; } = new List<Campaign>();

    private User() { }

    public User(string name, decimal emeraldBalance)
    {
        Id = Guid.NewGuid();
        Name = name;
        EmeraldBalance = emeraldBalance;
    }
    
    public User(Guid id, string name, decimal emeraldBalance)
    {
        Id = id;
        Name = name;
        EmeraldBalance = emeraldBalance;
    }

    public bool CanAfford(decimal amount) => EmeraldBalance >= amount;

    public void Deduct(decimal amount)
    {
        if (!CanAfford(amount))
            throw new InvalidOperationException("Insufficient funds.");
        EmeraldBalance -= amount;
    }

    public void AddFunds(decimal amount) => EmeraldBalance += amount;
}