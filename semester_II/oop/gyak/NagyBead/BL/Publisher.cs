using BL.Book;
using BL.People;

namespace BL
{
    public class Publisher : IPublisher
    {
        public IList<IBook> PublishedBooks { get; }

        private IList<IAuthor> _partnerAuthors;

        public Publisher()
        {
            _partnerAuthors = new List<IAuthor>();
            PublishedBooks = new List<IBook>();
        }
    
        public IBook Publish(string isbn, IDraft draft, DateTime publishedAt)
        {
            if (isbn == null) throw new ArgumentNullException(nameof(isbn));
            if (draft == null) throw new ArgumentNullException(nameof(draft));
            var title = draft.DraftTitle ?? throw new ArgumentNullException(nameof(draft.DraftTitle));

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

            return Publish(GenerateISBN(), draft);
        }
    
        private static string GenerateISBN()
        {
            var rnd = new Random();
            var isbn = "";
            for (var i = 0; i < 13; i++)
            {
                switch (i)
                {
                    case 1:
                    case 6:
                    case 11:
                        isbn += "-";
                        break;
                    default:
                        isbn += rnd.Next(0, 10).ToString();
                        break;
                }
            }
            return isbn;
        }
    }
}