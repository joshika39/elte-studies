namespace LibraryCore.Lib;

public class LateReturnBill : IBill
{
    private double _originalPrice;
    public Guid Id { get; }
    public double Amount { get; private set; }
    public bool IsPaid { get; private set; }
    public double Pay(double amount)
    {
        var rem = Amount - amount;
        if (rem < 0)
        {   
            IsPaid = true;
            Amount = 0;
            return Math.Abs(rem);
        }

        Amount -= amount;
        return 0;
    }

    public LateReturnBill(int amount)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        _originalPrice = amount;
    }

    public override string ToString()
    {
        if (IsPaid)
        {
            return $"Bill ID: {Id}\nOriginal amount: {Math.Round(_originalPrice * 1.00f, 2)}HUF\nStatus: Paid\n";
        }

        if (_originalPrice.Equals(Amount))
        {
            return $"Bill ID: {Id}\nRemaining amount: {Math.Round(Amount * 1.00f, 2)}HUF\nStatus: Not yet paid\n";
        }
        
        return $"Bill ID: {Id}\nOriginal amount: {Math.Round(_originalPrice * 1.00f, 2)}HUF\nRemaining amount: {Math.Round(Amount * 1.00f, 2)}HUF\nStatus: Not yet paid\n";
    }
}