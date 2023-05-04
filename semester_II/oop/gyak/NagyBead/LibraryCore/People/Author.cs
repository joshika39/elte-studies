using LibraryCore.Book;

namespace LibraryCore.People;

public class Author : IAuthor
{
    public string Name { get; }
    public DateTime BornAt { get; }
    public IList<IBook> Books { get; }

    public Author(string name, DateTime bornAt)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        BornAt = bornAt;
        Books = new List<IBook>();
    }
    
    public IDraft WriteBook(string title, int pageCount)
    {
        return new Draft(title, pageCount, this);
    }

    public void Contribute(IDraft book)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        if (!book.Contributors.Contains(this))
        {
            book.Contributors.Add(this);
        }
    }

    public IBook Publish(IPublisher publisher, IDraft book)
    {
        if (publisher == null) throw new ArgumentNullException(nameof(publisher));
        if (book == null) throw new ArgumentNullException(nameof(book));
        
        var pBook = publisher.Publish(book);
        Books.Add(pBook);
        
        return pBook;
    }
}