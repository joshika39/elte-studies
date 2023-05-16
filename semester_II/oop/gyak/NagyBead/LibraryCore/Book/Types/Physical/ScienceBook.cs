namespace LibraryCore.Book.Types.Physical;

public class ScienceBook : APhysicalBook
{

    public override void ValidateReturn(DateTime returnDate)
    {
        base.ValidateReturn(returnDate, 100);
    }
    public ScienceBook(IBook book, Guid libraryId, float preservation) : base(book, libraryId, preservation)
    { }
}