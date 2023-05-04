namespace LibraryCore.Book.Types.Online;

public class ELiteratureBook : AeBook
{
    public ELiteratureBook(IBook book, Guid libraryId, string bookId, double size, string format) 
        : base(book, libraryId, bookId, size, format)
    { }
    public override void ValidateReturn(DateTime returnDate)
    {
        ValidateReturn(returnDate, 30);
    }
}
