namespace LibraryCore.Book.Types.Online;

public class EYouthBook : AeBook
{
    public EYouthBook(IBook book, Guid libraryId, string bookId, double size, string format) 
        : base(book, libraryId, bookId, size, format)
    { }
    public override void ValidateReturn(DateTime returnDate)
    {
        base.ValidateReturn(returnDate, 50);
    }
}