using LibraryCore.Book;
using LibraryCore.People;

namespace LibraryCore
{
    public interface IPublisher
    {
        IList<IBook> PublishedBooks { get; }
        IBook Publish(string isbn, IDraft draft, DateTime publishedAt);
        IBook Publish(string isbn, IDraft draft);
        IBook Publish(IDraft draft);
    }
}
