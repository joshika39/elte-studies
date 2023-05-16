using LibraryCore.Book.Types;

namespace LibraryCore.Book.Factory
{
    public interface IBookFactory
    {
        IeBook CreateOnlineBook(IBook book, Guid libraryId, double size, string format, string category);
        IPhysicalBook CreateBook(IBook book, Guid libraryId, float preservation, string category);
        ILibraryBook CreateGeneralBook(IBook book, Guid libraryId, string category);
    }
}
