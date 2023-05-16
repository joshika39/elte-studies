﻿using Infrastructure.IO;
using LibraryCore.Book;
using LibraryCore.Book.Factory;
using LibraryCore.People;

namespace LibraryCore.Lib
{
    public class LibraryClass : ILibrary
    {
        private readonly IReader _reader;
        private readonly IList<ILibraryBook> _borrowedBooks;
        private readonly IBookFactory _bookFactory;
        private readonly IList<MemberDTO> _members;
        
        public IEnumerable<ILibraryBook> AvailableBooks => AllBooks.Where(b => !_borrowedBooks.Contains(b));
        public Guid Id { get; }
        public IList<ILibraryBook> AllBooks { get; }

        public LibraryClass(IBookFactory bookFactory, IReader reader)
        {
            _bookFactory = bookFactory ?? throw new ArgumentNullException(nameof(bookFactory));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Id = Guid.NewGuid();
            AllBooks = new List<ILibraryBook>();
            _borrowedBooks = new List<ILibraryBook>();
            _members = new List<MemberDTO>();
        }

        public IMember? Login(string username)
        {
            var memberDto = _members.FirstOrDefault(m => m.Member.UserName == username);
            if (memberDto != null)
            {
                return memberDto.Member;
            }

            Console.WriteLine($"No member is registered with the username: {username}");
            return default;
        }

        public void GetNewBook(IBook book, string category)
        {
            var libBook = _bookFactory.CreateGeneralBook(book, Id, category);
            AllBooks.Add(libBook);
        }

        public bool LendBook(IBook book, DateTime date, Guid memberId)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (date == default) throw new ArgumentNullException(nameof(date));
            if (!SearchUser(memberId, out var member))
            {
                Console.WriteLine($"No member was found with the id: {memberId}");
                return false;
            }

            if (SearchBook(book.ISBN, AvailableBooks, out var res))
            {
                res!.BorrowedAt = date;
                res.ReturnAt = date + new TimeSpan(21, 0, 0, 0);
                res.BorrowedBy = member;
                _borrowedBooks.Add(res);
                return true;
            }

            Console.WriteLine($"The book: {book.Title} ({book.ISBN}) was not found");
            return false;
        }

        public bool ReturnBook(IBook book, DateTime date, Guid memberId)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (!SearchUser(memberId, out _))
            {
                Console.WriteLine($"No member was found with the id: {memberId}");
                return false;
            }

            if (SearchBook(book.ISBN, _borrowedBooks, out var res))
            {
                res!.ValidateReturn(date);
                res.ReturnAt = default;
                res.BorrowedAt = default;
                res.BorrowedBy = default!;
                _borrowedBooks.Remove(res);
                return true;
            }

            Console.WriteLine($"The book: {book.Title} ({book.ISBN}) was not found");
            return false;
        }

        public void Register(string name, string username, DateTime bornAt)
        {
            if (_members.Count != 0 && _members.Any(m => m.Member.UserName == username))
            {
                return;
            }
            var guid = Guid.NewGuid();
            _members.Add(new MemberDTO(new Member(name, username, bornAt, this, guid), guid));
        }

        public void LoadBooks(string path)
        {
            foreach (var book in
                     _reader.ReadAllLines<ILibraryBook>(new StreamReader(path), BookFactory.LibBookTryParse))
            {
                GetNewBook(book);
            }
        }

        public void LoadMembers(string path)
        {
            foreach (var person in _reader.ReadAllLines<IMember>(new StreamReader(path), MemberTryParse))
            {
                Register(person.Name, person.UserName, person.BornAt);
            }
        }
        
        private void GetNewBook(ILibraryBook book)
        {
            if (AllBooks.Any(b => b.LibraryId != book.LibraryId))
            {
                AllBooks.Add(book);
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

        private bool SearchUser(Guid memberId, out IMember? member)
        {
            var memberDto = _members.FirstOrDefault(dto => dto.Id == memberId);
            if (memberDto != null)
            {
                member = memberDto.Member;
                return true;
            }

            member = default;
            return false;
        }

        private bool MemberTryParse(string line, out IMember member)
        {
            var data = line.Trim().Split(new[] { ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            member = new Member(data[0], data[1], DateTime.Parse(data[2]), this);
            return true;
        }
    }
}