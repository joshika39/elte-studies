using Infrastructure.IO;

namespace BL.Book.Types.Physical
{
    public class ScienceBook : APhysicalBook
    {

        public override void ValidateReturn(DateTime returnDate, IWriter writer)
        {
            base.ValidateReturn(returnDate, 100, writer);
        }
        public ScienceBook(IBook book, Guid libraryId, float preservation) : base(book, libraryId, preservation)
        { }
    }
}