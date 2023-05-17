using Infrastructure.IO;

namespace LibraryCore.Lib;

public class LateReturnBill : IBill
{
    private readonly double _originalPrice;
    private readonly IWriter _writer;
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

    public LateReturnBill(int amount, IWriter writer)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        _originalPrice = amount;
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));
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

    public void Print()
    {
        if (IsPaid)
        {
            _writer.WriteLine(Constants.EscapeColors.CYAN, $"Bill ID: {Id}");
            _writer.WriteLine(Constants.EscapeColors.CYAN, $"\tOriginal amount: {Math.Round(_originalPrice * 1.00f, 2)}HUF");
            _writer.WriteLine(Constants.EscapeColors.GREEN, $"\tStatus: Paid\n");
        }
        else
        {
            if (_originalPrice.Equals(Amount))
            {
                _writer.WriteLine(Constants.EscapeColors.CYAN, $"Bill ID: {Id}");
                _writer.WriteLine(Constants.EscapeColors.CYAN, $"\tRemaining amount: {Math.Round(Amount * 1.00f, 2)}HUF");
                _writer.WriteLine(Constants.EscapeColors.RED, $"\tStatus: Not yet paid\n");
            }
            else
            {
                _writer.WriteLine(Constants.EscapeColors.CYAN, $"Bill ID: {Id}");
                _writer.WriteLine(Constants.EscapeColors.CYAN, $"\tOriginal amount: {Math.Round(_originalPrice * 1.00f, 2)}HUF");
                _writer.WriteLine(Constants.EscapeColors.CYAN, $"\tRemaining amount: {Math.Round(Amount * 1.00f, 2)}HUF");
                _writer.WriteLine(Constants.EscapeColors.YELLOW, $"\tStatus: Not yet paid\n");
            }
        }
    }
}