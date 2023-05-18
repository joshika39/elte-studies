using Infrastructure.IO;
using BL.People;

namespace BL.Book;

public interface ILibraryBook : IBook
{
    Guid LibraryId { get; }
    IMember? BorrowedBy { get; set; }
    DateTime BorrowedAt { get; set; }
    DateTime ReturnAt { get; set; }
    void ValidateReturn(DateTime returnDate, IWriter writer);
}