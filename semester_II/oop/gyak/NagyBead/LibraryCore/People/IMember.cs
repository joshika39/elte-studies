using LibraryCore.Book;
using LibraryCore.Lib;

namespace LibraryCore.People;

public interface IMember
{
    string Name { get; }
    string UserName { get; }
    DateTime BornAt { get; }
    double Balance { get; set; }
    IEnumerable<ILibraryBook> BorrowedBooks { get; }
    IList<IBill> PendingBills { get; }
    
    void Borrow(ILibraryBook book);
    void Return(ILibraryBook book, DateTime date);
    void Pay(double amount);
    void Pay(IBill bill, double amount);
}