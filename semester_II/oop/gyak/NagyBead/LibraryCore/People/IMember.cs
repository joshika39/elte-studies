using LibraryCore.Book;
using LibraryCore.Lib;

namespace LibraryCore.People;

public interface IMember
{
    string Name { get; }
    string UserName { get; }
    DateTime BornAt { get; }
    IList<ILibraryBook> BorrowedBooks { get; }
    IList<IBill> PendingBills { get; }
    void Borrow(ILibraryBook book);
}