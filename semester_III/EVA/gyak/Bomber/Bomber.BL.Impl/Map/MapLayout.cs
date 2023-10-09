using Bomber.BL.Map;

namespace Bomber.BL.Impl.Map
{
    public class MapLayout : IMapLayout
    {

        public Guid Id
        {
            get;
        }
        public string Description
        {
            get;
        }
        public int ColumnCount
        {
            get;
        }
        public int RowCount
        {
            get;
        }
    }
}
