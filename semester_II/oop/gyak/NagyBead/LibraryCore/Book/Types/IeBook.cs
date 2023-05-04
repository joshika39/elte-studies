namespace LibraryCore.Book.Types
{
    public interface IeBook : ILibraryBook
    {
        double Size { get; }
        string Format { get; }
    }
}
