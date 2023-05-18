using BL.Book;

namespace BL.People
{
    public interface IAuthor
    {
        string Name { get; }
        DateTime BornAt { get; }
        IList<IBook>Books { get; }
        IDraft WriteBook(string title, int pageCount);
        void Contribute(IDraft book);
        IBook Publish(IPublisher publisher, IDraft book);
    }
}