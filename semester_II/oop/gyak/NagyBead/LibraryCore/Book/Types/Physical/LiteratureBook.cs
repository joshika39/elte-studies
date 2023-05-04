using LibraryCore.Book.Types.Online;

namespace LibraryCore.Book.Types.Physical;

public class LiteratureBook : APhysicalBook
{
    public LiteratureBook(IBook book, Guid libraryId, string bookId, float preservation)
        : base(book, libraryId, bookId, preservation)
    { }
    
    public override void ValidateReturn(DateTime returnDate)
    {
        ValidateReturn(returnDate, 30);
    }
   
}
