using Bomber.BL.Map;
using GameFramework.Entities;
using GameFramework.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class Map : IMap2D
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public IEnumerable<IPlayer2D> Players { get; }
        public IEnumerable<IMapObject2D> MapObjects { get; }

        public Map(IMapLayout layout)
        {
            Players = new List<IPlayer2D>();
            SizeY = layout.RowCount;
            SizeX = layout.ColumnCount;
            MapObjects = layout.MapObjects;
        }
    }
}
