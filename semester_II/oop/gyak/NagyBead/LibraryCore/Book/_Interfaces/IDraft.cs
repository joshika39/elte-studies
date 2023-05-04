using LibraryCore.People;

namespace LibraryCore.Book;

public interface IDraft
{
    string DraftTitle { get; }
    int DraftPageCount { get; }
    IList<IAuthor> Contributors { get; }
}