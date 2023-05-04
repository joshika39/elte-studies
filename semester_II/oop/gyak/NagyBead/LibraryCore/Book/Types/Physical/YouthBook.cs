namespace LibraryCore.Book.Types.Physical;

public class YouthBook : APhysicalBook
{
    public YouthBook(IBook book, Guid libraryId, string bookId, float preservation) 
        : base(book, libraryId, bookId, preservation)
    { }
    public override void ValidateReturn(DateTime returnDate)
    {
        base.ValidateReturn(returnDate, 50);
    }
    
}