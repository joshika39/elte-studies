using System.Collections.Immutable;
using Infrastructure.IO;
using BL.Book;
using BL.Lib;

namespace BL.People;

public interface IMember
{
    string Name { get; }
    string UserName { get; }
    DateTime BornAt { get; }
    double Balance { get; set; }
    IImmutableList<ILibraryBook> BorrowedBooks { get; }
    IList<IBill> PendingBills { get; }
    
    void Borrow(ILibraryBook book);
    void Return(ILibraryBook book, DateTime date);
    void Pay();
    void Pay(double amount);
    void Pay(IBill bill, double amount);
    void PrintDetails(IWriter writer);
}