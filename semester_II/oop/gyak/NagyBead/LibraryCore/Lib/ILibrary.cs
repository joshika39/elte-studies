using LibraryCore.Book;
using LibraryCore.People;

namespace LibraryCore.Lib;

public interface ILibrary
{
    Guid Id { get; }
    IList<ILibraryBook> AllBooks { get; }
    IList<IMember> Members { get; }
    void GetNewBook(IBook book, string category);
    void LendBook(IMember member, IBook book, DateTime date);
    void ReturnBook(IMember member, IBook book, DateTime date);
    void Register(string name, string username, DateTime bornAt);
    void Register(IMember member);
}