using Bomber.BL.Map;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Tiles
{
    public interface IPlaceHolder : IMapObject2D
    {
        TileType Type { get; }
    }
}
