namespace BL.Lib
{
    public interface IBill
    {
        Guid Id { get; }
        double Amount { get; }
        bool IsPaid { get; }
        double Pay(double amount);
        void Print();
    }
}