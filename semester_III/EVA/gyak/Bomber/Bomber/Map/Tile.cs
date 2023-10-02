using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.Map
{
    public abstract class ATile : IMapObject2D
    {
        public ATile(int x, int y, int ratio)
        {
            Position = new ;
        }
        public IPosition2D Position { get; }
    }
}
