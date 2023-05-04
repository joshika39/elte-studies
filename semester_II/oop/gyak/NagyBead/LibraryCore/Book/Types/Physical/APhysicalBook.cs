namespace LibraryCore.Book.Types.Physical
{
    public abstract class APhysicalBook : ABook, IPhysicalBook
    {

        protected APhysicalBook(IBook book, Guid libraryId, string bookId, float preservation) 
            : base(book, libraryId, bookId)
        {
            Preservation = preservation;
        }
        public float Preservation
        {
            get;
        }

    }
}
