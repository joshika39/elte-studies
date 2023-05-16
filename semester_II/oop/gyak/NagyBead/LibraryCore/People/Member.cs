using LibraryCore.Book;
using LibraryCore.Lib;

namespace LibraryCore.People;

public class Member : IMember
{
    #region Private Fields

    private readonly ILibrary _lib;
    private readonly IList<ILibraryBook> _borrowedBooks;
    private readonly Guid _id;

    #endregion

    public string Name { get; }
    public string UserName { get; }
    public DateTime BornAt { get; }
    public double Balance { get; set; }
    public IEnumerable<ILibraryBook> BorrowedBooks => _borrowedBooks;
    public IList<IBill> PendingBills { get; }


    public Member(string name, string userName, DateTime bornAt, ILibrary lib)
    {
        _lib = lib ?? throw new ArgumentNullException(nameof(lib));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        BornAt = bornAt;
        _borrowedBooks = new List<ILibraryBook>();
        PendingBills = new List<IBill>();
        Balance = 0;
    }

    public Member(string name, string userName, DateTime bornAt, ILibrary lib, Guid id)
    {
        _lib = lib ?? throw new ArgumentNullException(nameof(lib));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _id = id;
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        BornAt = bornAt;
        _borrowedBooks = new List<ILibraryBook>();
        PendingBills = new List<IBill>();
        Balance = 0;
    }

    public override string ToString()
    {
        return $"Name: {Name}\nUsername: {UserName}\nBalance: {Balance}HUF\n";
    }

    public void Borrow(ILibraryBook book)
    {
        if (_lib.LendBook(book, DateTime.Now, _id))
        {
            _borrowedBooks.Add(book);
        }
    }

    public void Return(ILibraryBook book, DateTime date)
    {
        if (_lib.ReturnBook(book, date, _id))
        {
            _borrowedBooks.Remove(book);
        }
    }

    public void Pay()
    {
        if (PendingBills.Count > 0)
        {
            Pay(PendingBills[0], Balance);
        }
    }
    
    public void Pay(double amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("Not enough money in your account.");
            return;
        }
        
        if (PendingBills.Count > 0)
        {
            Pay(PendingBills[0], amount);
        }
    }

    public void Pay(IBill bill, double amount)
    {
        if (PendingBills.Any(b => b.Id == bill.Id))
        {
            Balance = bill.Pay(amount);
        }
    }
}