using Infrastructure.IO;
using LibraryCore.Book.Types;
using LibraryCore.Book.Types.Online;
using LibraryCore.Book.Types.Physical;
using LibraryCore.People;

namespace LibraryCore.Book.Factory
{
    public class BookFactory : IBookFactory
    {
        public IeBook CreateOnlineBook(IBook book, Guid libraryId, double size, string format, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (format == null) throw new ArgumentNullException(nameof(format));
            if (category == null) throw new ArgumentNullException(nameof(category));
            IeBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new EScienceBook(book, libraryId, size, format);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new ELiteratureBook(book, libraryId, size, format);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new EYouthBook(book, libraryId, size, format);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }
        
        public IPhysicalBook CreateBook(IBook book, Guid libraryId, float preservation, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            IPhysicalBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new ScienceBook(book, libraryId, preservation);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new LiteratureBook(book, libraryId, preservation);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new YouthBook(book, libraryId, preservation);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }

        public ILibraryBook CreateGeneralBook(IBook book, Guid libraryId, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            ILibraryBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new ScienceBook(book, libraryId, 100);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new LiteratureBook(book, libraryId, 100);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new YouthBook(book, libraryId, 100);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }

        public static bool LibBookTryParse(string line, out ILibraryBook libBook)
        {
            var data = line.Trim().Split(new []{';', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            var authors = new List<IAuthor>();
            
            foreach (var authorStr in data[6].Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                authors.Add(new Author(authorStr, new DateTime()));
            }

            var book = new PublishedBook(
                data[1], 
                data[2], 
                int.Parse(data[3]), 
                DateTime.Parse(data[5]), 
                authors.ToArray());

            switch (data[0])
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new ScienceBook(book, Guid.Parse(data[4]), 100);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new LiteratureBook(book, Guid.Parse(data[4]), 100);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new YouthBook(book, Guid.Parse(data[4]), 100);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }

            return true;
        }
    }
}
