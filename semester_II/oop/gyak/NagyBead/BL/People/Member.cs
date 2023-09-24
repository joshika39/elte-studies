using System.Collections.Immutable;
using Infrastructure.IO;
using BL.Book;
using BL.Lib;

namespace BL.People
{
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
        public IImmutableList<ILibraryBook> BorrowedBooks => _borrowedBooks.ToImmutableList();
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
            if (PendingBills.All(b => b.Id != bill.Id)) return;
            Balance -= amount;
            Balance += bill.Pay(amount);
        }

        public void PrintDetails(IWriter writer)
        {
        
            writer.WriteLine(Implementation.Constants.EscapeColors.YELLOW, $"Personal details: ");
            writer.WriteLine(Implementation.Constants.EscapeColors.GREEN, $"\tName: {Name}");
            writer.WriteLine(Implementation.Constants.EscapeColors.GREEN, $"\tUsername: {UserName}");
            writer.WriteLine(Implementation.Constants.EscapeColors.GREEN, $"\tBorn At: {BornAt}");
            writer.WriteLine(Implementation.Constants.EscapeColors.CYAN, $"\tBalance: {Balance}\n");
        
            writer.WriteLine(Implementation.Constants.EscapeColors.YELLOW, $"Borrowed books: ");
            foreach (var book in _borrowedBooks)
            {
                writer.WriteLine(Implementation.Constants.EscapeColors.CYAN, $"Title: {book.Title}");
                writer.WriteLine(Implementation.Constants.EscapeColors.GREEN, $"\tBorrowed date: {book.BorrowedAt}");
                writer.WriteLine(Implementation.Constants.EscapeColors.GREEN, $"\tReturn due: {book.BorrowedAt + new TimeSpan(15, 0, 0, 0)}");
            }
            writer.WriteLine("");
        
            writer.WriteLine(Implementation.Constants.EscapeColors.YELLOW, $"Pending bills: ");
            foreach (var bill in PendingBills)
            {
                bill.Print();
            }

        }
    }
}