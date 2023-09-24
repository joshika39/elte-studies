namespace HF3
{
    public interface IContent
    {
        string Name { get; }
        int GetSize();
        void Rename(string newName);
    }
}
