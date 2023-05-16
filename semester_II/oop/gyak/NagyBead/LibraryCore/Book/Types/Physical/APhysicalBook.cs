namespace LibraryCore.Book.Types.Physical
{
    public abstract class APhysicalBook : ABook, IPhysicalBook
    {

        protected APhysicalBook(IBook book, Guid libraryId, float preservation) 
            : base(book, libraryId)
        {
            Preservation = preservation;
        }
        public float Preservation
        {
            get;
        }

    }
}
