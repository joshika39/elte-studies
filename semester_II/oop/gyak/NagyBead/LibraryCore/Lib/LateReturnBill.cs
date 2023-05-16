namespace LibraryCore.Lib;

public class LateReturnBill : IBill
{    
    public Guid Id { get; }
    public int Amount { get; }

    public LateReturnBill(int amount)
    {
        Id = Guid.NewGuid();
        Amount = amount;
    }

    public override string ToString()
    {
        return $"Bill ID: {Id}\nAmount: ${Amount}";
    }
}