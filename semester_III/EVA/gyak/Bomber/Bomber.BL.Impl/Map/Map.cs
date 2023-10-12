using GameFramework.Entities;
using GameFramework.Impl.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class Map : AMap2D
    {
        public Map(int sizeX, int sizeY, IList<IUnit2D> entities, IEnumerable<IMapObject2D> mapObjects) : base(sizeX, sizeY, entities, mapObjects)
        { }
    }
}
