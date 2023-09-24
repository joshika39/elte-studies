using BL.Book;
using BL.People;

namespace BL
{
    public interface IPublisher
    {
        IList<IBook> PublishedBooks { get; }
        IBook Publish(string isbn, IDraft draft, DateTime publishedAt);
        IBook Publish(string isbn, IDraft draft);
        IBook Publish(IDraft draft);
    }
}
