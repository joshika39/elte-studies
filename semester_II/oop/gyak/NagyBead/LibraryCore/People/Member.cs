
using LibraryCore.Book;
using LibraryCore.Lib;

namespace LibraryCore.People;

public class Member : IMember
{
    private ILibrary _lib;
    public string Name { get; }
    public string UserName { get; }
    public DateTime BornAt { get; }
    public IList<ILibraryBook> BorrowedBooks { get; }
    public IList<IBill> PendingBills { get; }


    public Member(string name, string userName, DateTime bornAt, ILibrary lib)
    {
        _lib = lib ?? throw new ArgumentNullException(nameof(lib));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        BornAt = bornAt;
        BorrowedBooks = new List<ILibraryBook>();
        PendingBills = new List<IBill>();
    }
    
    public void Borrow(ILibraryBook book)
    {
        throw new NotImplementedException();
    }
}