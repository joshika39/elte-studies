namespace LibraryCore.Lib;

public interface IBill
{
    Guid Id { get; }
    int Amount { get; }
}