using LibraryCore.People;

namespace LibraryCore.Book;

public interface ILibraryBook : IBook
{
    Guid LibraryId { get; }
    string BookId { get; }
    IMember BorrowedBy { get; set; }
    DateTime BorrowedAt { get; set; }
    DateTime ReturnAt { get; set; }
    void ValidateReturn(DateTime returnDate);
}