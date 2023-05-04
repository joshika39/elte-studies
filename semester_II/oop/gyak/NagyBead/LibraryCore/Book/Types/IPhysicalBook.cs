namespace LibraryCore.Book.Types
{
    public interface IPhysicalBook : ILibraryBook
    {
        float Preservation { get; }
    }
}
