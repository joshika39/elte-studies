using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.Map
{
    public abstract class ATile : IMapObject2D
    {
        public IPosition2D Position { get; }

        public ATile(IPosition2D position2D, int ratio)
        {
            Position = position2D ?? throw new ArgumentNullException(nameof(position2D));
        }
    }
}
