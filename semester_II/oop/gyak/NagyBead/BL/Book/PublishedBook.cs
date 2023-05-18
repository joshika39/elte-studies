using BL.People;

namespace BL.Book
{
    public class PublishedBook : IBook
    {
        public string ISBN { get; }
        public string Title { get; }
        public int PageCount { get; }
        public DateTime PublishedAt { get; }
        public IAuthor MainAuthor => Authors.FirstOrDefault()!;
        public IList<IAuthor> Authors { get; }

        public PublishedBook(string isbn, string title, int pageCount, DateTime publishedAt, params IAuthor[] authors)
        {
            ISBN = isbn;
            Title = title;
            PageCount = pageCount;
            PublishedAt = publishedAt;
            Authors = authors.ToList();
        }
    }
}