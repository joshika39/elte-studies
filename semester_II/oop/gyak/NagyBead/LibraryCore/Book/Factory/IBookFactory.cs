using LibraryCore.Book.Types;

namespace LibraryCore.Book.Factory
{
    public interface IBookFactory
    {
        IeBook CreateOnlineBook(IBook book, Guid libraryId, string bookId, double size, string format, string category);
        IPhysicalBook CreateBook(IBook book, Guid libraryId, string bookId, float preservation, string category);
        ILibraryBook CreateGeneralBook(IBook book, Guid libraryId, string bookId, string category);
    }
}
