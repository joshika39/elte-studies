using System.Text;
using BL.Book;
using BL.People;

namespace BL
{
    public class Publisher : IPublisher
    {
        public IList<IBook> PublishedBooks { get; }

        private readonly IList<IAuthor> _partnerAuthors;

        public Publisher()
        {
            _partnerAuthors = new List<IAuthor>();
            PublishedBooks = new List<IBook>();
        }
    
        public IBook Publish(string isbn, IDraft draft, DateTime publishedAt)
        {
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (draft == null) throw new ArgumentNullException(nameof(draft));
            var title = draft.DraftTitle ?? throw new ArgumentNullException(nameof(draft));

            _partnerAuthors.Add(draft.Contributors.FirstOrDefault()!);
            var book = new PublishedBook(isbn, title, draft.DraftPageCount, publishedAt, draft.Contributors.ToArray());
            PublishedBooks.Add(book);
            return book;
        }

        public IBook Publish(string isbn, IDraft draft)
        {
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (draft == null) throw new ArgumentNullException(nameof(draft));

            return Publish(isbn, draft, DateTime.Now);
        }

        public IBook Publish(IDraft draft)
        {
            if (draft == null) throw new ArgumentNullException(nameof(draft));

            return Publish(GenerateIsbn(), draft);
        }
    
        private static string GenerateIsbn()
        {
            var stringBuilder = new StringBuilder();
            var rnd = new Random();
            for (var i = 0; i < 13; i++)
            {
                switch (i)
                {
                    case 1:
                    case 6:
                    case 11:
                        stringBuilder.Append('-');
                        break;
                    default:
                        stringBuilder.Append(rnd.Next(0, 10));
                        break;
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}