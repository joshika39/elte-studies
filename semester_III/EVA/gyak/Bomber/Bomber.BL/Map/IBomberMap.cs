using GameFramework.Core;
using GameFramework.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Map
{
    public interface IBomberMap : IMap2D
    {
        ICollection<INpc> NPCs { get; }
        
        bool HasEnemy(IPosition2D position);
        
        IEnumerable<IMapObject2D> MapPortion(IPosition2D topLeft, IPosition2D bottomRight);
        IEnumerable<IMapObject2D> MapPortion(IPosition2D center, int radius);
    }
}
