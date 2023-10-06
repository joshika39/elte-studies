using Bomber.BL.Map;

namespace Bomber.BL.Impl.Map
{
    public class MapLayoutDraft : IMapLayoutDraft
    {

        public Guid Id { get; } = Guid.NewGuid();
        public string Description { get; private set; } = "";
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        
        public void SetRow(int rowCount)
        {
            RowCount = rowCount;
        }
        public void SetCol(int colCount)
        {
            ColumnCount = colCount;
        }
        public void SetDesc(string description)
        {
            Description = description;
        }
    }
}
