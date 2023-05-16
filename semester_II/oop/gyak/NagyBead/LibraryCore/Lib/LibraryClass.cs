using Infrastructure.IO;
using LibraryCore.Book;
using LibraryCore.Book.Factory;
using LibraryCore.People;

namespace LibraryCore.Lib
{
    public class LibraryClass : ILibrary
    {
        private readonly IReader _reader;
        public Guid Id { get; }
        public IList<ILibraryBook> AllBooks { get; }
        private IEnumerable<ILibraryBook> AvailableBooks => AllBooks.Where(b => !BorrowedBooks.Contains(b));
    
        private IList<ILibraryBook> BorrowedBooks { get; }
        private readonly IBookFactory _bookFactory;
    
        public IList<IMember> Members { get; }

        public LibraryClass(IBookFactory bookFactory, IReader reader)
        {
            _bookFactory = bookFactory ?? throw new ArgumentNullException(nameof(bookFactory));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Id = Guid.NewGuid();
            AllBooks = new List<ILibraryBook>();
            BorrowedBooks = new List<ILibraryBook>();
            Members = new List<IMember>();
        }
    
        public void GetNewBook(IBook book, string category)
        {
            var libBook = _bookFactory.CreateGeneralBook(book, Id, Guid.NewGuid().ToString(), category);
            AllBooks.Add(libBook);
        }
    
        public void GetNewBook(ILibraryBook book)
        {
            if (AllBooks.All(b => b.LibraryId != book.LibraryId))
            {
                AllBooks.Add(book);
            }
        }

        public void LendBook(IMember member, IBook book, DateTime date)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (date == default) throw new ArgumentNullException(nameof(date));
        
            if (SearchBook(book.ISBN, AvailableBooks, out var res))
            {
                res!.BorrowedAt = date;
                res.ReturnAt = date + new TimeSpan(21, 0, 0, 0);
                res.BorrowedBy = member;
            
                member.BorrowedBooks.Add(res);
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

            if (SearchBook(book.ISBN, BorrowedBooks, out var res))
            {
                res!.ValidateReturn(date);
        
                member.BorrowedBooks.Remove(res);
        
                res.ReturnAt = default;
                res.BorrowedAt = default;
                res.BorrowedBy = default!;
        
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
            if (Members.Count == 0 || Members.Any(m => m.UserName != member.UserName))
            {
                Members.Add(member);
            }
        }
        
        public void LoadBooks(string path)
        {
            foreach (var book in _reader.ReadAllLines<ILibraryBook>(new StreamReader(path), BookFactory.LibBookTryParse))
            {
                GetNewBook(book);
            }
        }
        
        public void LoadMembers(string path)
        {
            foreach (var person in _reader.ReadAllLines<IMember>(new StreamReader(path), MemberTryParse))
            {
                Register(person);
            }
        }

        private bool SearchBook(string isbn, IEnumerable<ILibraryBook> target, out ILibraryBook? result)
        {
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (target == null) throw new ArgumentNullException(nameof(target));
            var list = target.ToList();
            if (list.Any(b => b.ISBN == isbn))
            {
                result = list.FirstOrDefault(b => b.ISBN == isbn);
                return true;
            }
            result = default;
            return false;
        }

        private bool MemberTryParse(string line, out IMember member)
        {
            var data = line.Trim().Split(new []{';', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            member = new Member(data[0], data[1], DateTime.Parse(data[2]), this);
            return true;
        }
    }
}