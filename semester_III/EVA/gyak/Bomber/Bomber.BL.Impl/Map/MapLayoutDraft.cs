using Bomber.BL.Map;
using Implementation.Repositories;

namespace Bomber.BL.Impl.Map
{
    public class MapLayoutDraft : AEntity, IMapLayoutDraft
    {
        public MapLayoutDraft() : base()
        {
            Description = "";
        }

        public MapLayoutDraft(string description) : base()
        {
            Description = description;
        }
        
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}
