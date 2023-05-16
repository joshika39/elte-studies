namespace LibraryCore.Lib;

public interface IBill
{
    Guid Id { get; }
    double Amount { get; }
    bool IsPaid { get; }
    double Pay(double amount);
}