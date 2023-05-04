using Library;
using LibraryCore.Book;
using LibraryCore.Book.Factory;
using LibraryCore.Book.Types;
using LibraryCore.Book.Types.Online;
using LibraryCore.People;

namespace LibraryCore.Lib;

public class LibraryClass : ILibrary
{
    public Guid Id { get; }
    public IList<ILibraryBook> AllBooks { get; private set; }
    public IList<ILibraryBook> AvailableBooks { get; }
    
    private IList<ILibraryBook> BorrowedBooks { get; }
    private readonly IBookFactory _bookFactory;
    
    public IList<IMember> Members { get; }

    public LibraryClass(IBookFactory bookFactory)
    {
        _bookFactory = bookFactory ?? throw new ArgumentNullException(nameof(bookFactory));
        Id = Guid.NewGuid();
        AllBooks = new List<ILibraryBook>();
        AvailableBooks = new List<ILibraryBook>();
        BorrowedBooks = new List<ILibraryBook>();
        Members = new List<IMember>();
    }
    
    public void GetNewBook(IBook book, string category)
    {
        _bookFactory.CreateGeneralBook(book, Id, Guid.NewGuid().ToString(), category);
    }

    public void LendBook(IMember member, IBook book, DateTime date)
    {
        if (member == null) throw new ArgumentNullException(nameof(member));
        if (book == null) throw new ArgumentNullException(nameof(book));
        if (date == default) throw new ArgumentNullException(nameof(date));
        
        var found = SearchBook(book.ISBN, AvailableBooks, out var res);
        if (found)
        {
            res.BorrowedAt = date;
            res.ReturnAt = date + new TimeSpan(21, 0, 0, 0);
            res.BorrowedBy = member;
            
            member.BorrowedBooks.Add(res);
            AvailableBooks.Remove(res);
            BorrowedBooks.Add(res);
        }
        else
        {
            Console.WriteLine($"The book: {book.Title} ({book.ISBN}) was not found");
        }
    }

    public void ReturnBook(IMember member, IBook book, DateTime date)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        var found = SearchBook(book.ISBN, BorrowedBooks, out var res);
        if (found)
        {
            res.ValidateReturn(date);
        
            member.BorrowedBooks.Remove(res);
        
            res.ReturnAt = default;
            res.BorrowedAt = default;
            res.BorrowedBy = default!;
        
            AvailableBooks.Add(res);
            BorrowedBooks.Remove(res);
        }
        else
        {
            Console.WriteLine($"The book: {book.Title} ({book.ISBN}) was not found");
        }
    }

    public void Register(string name, string username, DateTime bornAt)
    {
        Register(new Member(name, username, bornAt, this));
    }
    
    public void Register(IMember member)
    {
        Members.Add(member);
    }

    private bool SearchBook(string isbn, IEnumerable<ILibraryBook> target, out ILibraryBook result)
    {
        if (isbn == null) throw new ArgumentNullException(nameof(isbn));
        if (target == null) throw new ArgumentNullException(nameof(target));
        
        result = target.FirstOrDefault(b => b.ISBN == isbn);
        return result is not null;
    }
}