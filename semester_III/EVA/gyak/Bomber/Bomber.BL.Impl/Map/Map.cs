using Bomber.BL.Entities;
using Bomber.BL.Map;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using GameFramework.Impl.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Map
{
    public class Map : AMap2D, IBomberMap
    {
        private readonly IPositionFactory _positionFactory;
        public ICollection<INpc> NPCs { get; }

        public Map(int sizeX, int sizeY, ICollection<IUnit2D> entities, IEnumerable<IMapObject2D> mapObjects, IPositionFactory positionFactory) : base(sizeX, sizeY, entities, mapObjects)
        {
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            NPCs = new List<INpc>();
        }
        
        public bool HasEnemy(IPosition2D position)
        {
            return NPCs.Any(npc => position.Equals(npc.Position));
        }
        
        public IEnumerable<IMapObject2D> MapPortion(IPosition2D topLeft, IPosition2D bottomRight)
        {
            var objects = MapObjects.ToArray();
            for (var y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                for (var x = topLeft.X; x <= bottomRight.X; x++)
                {
                    yield return objects[y * SizeX + x];
                }
            }
        }
        
        public IEnumerable<IMapObject2D> MapPortion(IPosition2D center, int radius)
        {
            var top = center.Y - radius < 0 ? 0 : center.Y - radius;
            var bottom = center.Y + radius >= SizeY ? SizeY - 1 : center.Y + radius;
            var left = center.X - radius < 0 ? 0 : center.X - radius;
            var right = center.X + radius >= SizeX ? SizeX - 1 : center.X + radius;
            var topLeftPos = _positionFactory.CreatePosition(left, top);
            var bottomRightPos = _positionFactory.CreatePosition(right, bottom);
            return MapPortion(topLeftPos, bottomRightPos);
        }
    }
}
