namespace LibraryCore.Lib;

public class LateReturnBill : IBill
{    
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
        return amount;
    }

    public LateReturnBill(int amount)
    {
        Id = Guid.NewGuid();
        Amount = amount;
    }

    public override string ToString()
    {
        var payStr = IsPaid ? "Payed" : "Not yet paid";
        return $"Bill ID: {Id}\nRemaining amount: {Math.Round(Amount * 1.00f, 2)}HUF\nStatus: {payStr}\n";
    }
}