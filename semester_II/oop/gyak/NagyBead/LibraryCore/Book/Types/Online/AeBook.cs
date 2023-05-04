namespace LibraryCore.Book.Types.Online
{
    public abstract class AeBook : ABook, IeBook
    {
        public double Size { get; }
        public string Format { get; }
        
        protected AeBook(IBook book, Guid libraryId, string bookId, double size, string format) 
            : base(book, libraryId, bookId)
        {
            Size = size;
            Format = format ?? throw new ArgumentNullException(nameof(format));
        }
    }
}
