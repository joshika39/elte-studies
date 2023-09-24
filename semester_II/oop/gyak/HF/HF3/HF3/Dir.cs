namespace HF3
{
    public class Dir : IContent
    {
        public IList<IContent> Content { get; set; }
        public string Name { get; private set; }

        public Dir(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Content = new List<IContent>();
        }

        public int GetSize()
        {
            return Content.Sum(content => content.GetSize());
        }
        
        public void Rename(string newName)
        {
            Name = newName;
        }

        public IEnumerable<IContent> Search(string name)
        {
            return Content.Where(c => c.Name.Contains(name));
        }

        public void PrintDir(string indent="")
        {
            
            Console.WriteLine($"│{indent}└─ [{GetSize()}] {Name}");
            indent += "─ ";
            foreach (var c in Content)
            {
                switch (c)
                {
                    case OwnFile:
                        Console.WriteLine($"│├─{indent} [{c.GetSize()}] {c.Name}");
                        break;
                    case Dir dir:
                        dir.PrintDir("  ");
                        break;
                }
            }
        }
    }
}
