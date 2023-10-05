using Bomber.BL.Map;

namespace Bomber.BL.Impl.Map
{
    public class MapLayoutDraft : IMapLayoutDraft
    {

        public Guid Id { get; } = Guid.NewGuid();
        public string Description { get; set; } = "";
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}
