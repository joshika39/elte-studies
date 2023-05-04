﻿using LibraryCore.Lib;
using LibraryCore.People;

namespace LibraryCore.Book;

public abstract class ABook : ILibraryBook
{
    public string ISBN { get; }
    public string Title { get; }
    public int PageCount { get; }
    public DateTime PublishedAt { get; }
    public IAuthor MainAuthor => Authors.FirstOrDefault()!;
    public IList<IAuthor> Authors { get; }
    public Guid LibraryId { get; }
    public string BookId { get; }
    public IMember BorrowedBy { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime ReturnAt { get; set; }

    protected ABook(IBook book, Guid libraryId, string bookId)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        
        LibraryId = libraryId;
        BookId = bookId;
        ISBN = book.ISBN;
        Title = book.Title;
        PageCount = book.PageCount;
        PublishedAt = book.PublishedAt;
        Authors = book.Authors;
    }
    
    public abstract void ValidateReturn(DateTime returnDate);

    protected void ValidateReturn(DateTime returnDate, int amount)
    {
        if (BorrowedBy is null)
        {
            throw new InvalidOperationException("Something went wrong with the return validation.");
        }
        
        if (returnDate <= ReturnAt)
        {
            return;
        }

        var daysDue = new DateTime(returnDate.Ticks - ReturnAt.Ticks).Day;
        
        BorrowedBy.PendingBills.Add(new LateReturnBill(amount * daysDue));
    }
}