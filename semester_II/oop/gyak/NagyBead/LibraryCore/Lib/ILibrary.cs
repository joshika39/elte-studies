using LibraryCore.Book;
using LibraryCore.People;

namespace LibraryCore.Lib
{
    public interface ILibrary
    {
        Guid Id { get; }
        IList<ILibraryBook> AllBooks { get; }
        IEnumerable<ILibraryBook> AvailableBooks { get; }
        IMember? Login(string username);
        void GetNewBook(IBook book, string category);
        bool LendBook(IBook book, DateTime date, Guid memberId);
        bool ReturnBook(IBook book, DateTime date, Guid memberId);
        void Register(string name, string username, DateTime bornAt);
        
        void LoadBooks(string path);
        void LoadMembers(string path);
    }
}