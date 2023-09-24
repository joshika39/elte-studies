using BL.People;

namespace BL.Book;

public interface IDraft
{
    string DraftTitle { get; }
    int DraftPageCount { get; }
    IList<IAuthor> Contributors { get; }
}