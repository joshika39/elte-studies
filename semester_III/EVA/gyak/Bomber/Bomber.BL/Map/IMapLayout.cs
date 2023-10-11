using GameFramework.Map.MapObject;
using Infrastructure.Repositories;

namespace Bomber.BL.Map
{
    public interface IMapLayout
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        IEnumerable<IMapObject2D> MapObjects { get; }
    }
}
