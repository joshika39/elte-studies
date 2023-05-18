namespace BL.Book.Types
{
    public interface IPhysicalBook : ILibraryBook
    {
        float Preservation { get; }
    }
}
