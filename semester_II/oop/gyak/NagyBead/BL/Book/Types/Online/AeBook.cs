namespace BL.Book.Types.Online
{
    public abstract class AeBook : ABook, IeBook
    {
        public double Size { get; }
        public string Format { get; }
        
        protected AeBook(IBook book, Guid libraryId, double size, string format) 
            : base(book, libraryId)
        {
            Size = size;
            Format = format ?? throw new ArgumentNullException(nameof(format));
        }
    }
}
