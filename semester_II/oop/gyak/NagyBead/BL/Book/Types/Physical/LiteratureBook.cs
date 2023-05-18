using Infrastructure.IO;
using BL.Book.Types.Online;

namespace BL.Book.Types.Physical
{
    public class LiteratureBook : APhysicalBook
    {
        public LiteratureBook(IBook book, Guid libraryId, float preservation)
            : base(book, libraryId, preservation)
        { }
    
        public override void ValidateReturn(DateTime returnDate, IWriter writer)
        {
            ValidateReturn(returnDate, 30, writer);
        }
   
    }
}
