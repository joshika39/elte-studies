using LibraryCore.People;

namespace LibraryCore.Book;

public interface IBook
{
    string ISBN { get; } 
    string Title { get; } 
    int PageCount { get; }
    DateTime PublishedAt { get; }
    IAuthor MainAuthor { get; }
    IList<IAuthor> Authors { get; }
}