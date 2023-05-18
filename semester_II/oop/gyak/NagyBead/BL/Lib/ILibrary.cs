using System.Collections.Immutable;
using BL.Book;
using BL.People;

namespace BL.Lib
{
    public interface ILibrary
    {
        Guid Id { get; }
        IEnumerable<ILibraryBook> AllBooks { get; }
        IImmutableList<ILibraryBook> AvailableBooks { get; }
        IMember? Login(string username);
        void GetNewBook(IBook book, string category);
        bool LendBook(IBook book, DateTime date, Guid memberId);
        bool ReturnBook(IBook book, DateTime date, Guid memberId);
        void Register(string name, string username, DateTime bornAt);
        void LoadBooks(string path);
        void LoadMembers(string path);
    }
}