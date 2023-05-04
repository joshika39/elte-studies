using Library;
using LibraryCore.Book.Types;
using LibraryCore.Book.Types.Online;
using LibraryCore.Book.Types.Physical;

namespace LibraryCore.Book.Factory
{
    public class BookFactory : IBookFactory
    {

        public IeBook CreateOnlineBook(IBook book, Guid libraryId, string bookId, double size, string format, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (bookId == null) throw new ArgumentNullException(nameof(bookId));
            if (format == null) throw new ArgumentNullException(nameof(format));
            if (category == null) throw new ArgumentNullException(nameof(category));
            IeBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new EScienceBook(book, libraryId, bookId, size, format);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new ELiteratureBook(book, libraryId, bookId, size, format);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new EYouthBook(book, libraryId, bookId, size, format);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }
        
        public IPhysicalBook CreateBook(IBook book, Guid libraryId, string bookId, float preservation, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (bookId == null) throw new ArgumentNullException(nameof(bookId));
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            IPhysicalBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new ScienceBook(book, libraryId, bookId, preservation);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new LiteratureBook(book, libraryId, bookId, preservation);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new YouthBook(book, libraryId, bookId, preservation);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }

        public ILibraryBook CreateGeneralBook(IBook book, Guid libraryId, string bookId, string category)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (bookId == null) throw new ArgumentNullException(nameof(bookId));
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            ILibraryBook libBook;
            switch (category)
            {
                case Constants.BookTypes.SCIENCE_BOOK:
                    libBook = new ScienceBook(book, libraryId, bookId, 100);
                    break;
                case Constants.BookTypes.LITERATURE_BOOK:
                    libBook = new LiteratureBook(book, libraryId, bookId, 100);
                    break;
                case Constants.BookTypes.YOUTH_BOOK:
                    libBook = new YouthBook(book, libraryId, bookId, 100);
                    break;
                default:
                    throw new InvalidOperationException("Invalid book category");
            }
            return libBook;
        }
    }
}
