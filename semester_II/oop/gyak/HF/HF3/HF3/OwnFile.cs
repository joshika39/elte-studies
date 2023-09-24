namespace HF3
{
    public class OwnFile : IContent
    {
        private readonly int _size;
        public string Name { get; private set; }

        public OwnFile(string name, int size)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            _size = size;
        }
        
        public int GetSize()
        {
            return _size;
        }
        public void Rename(string newName)
        {
            Name = newName;
        }
    }
}
