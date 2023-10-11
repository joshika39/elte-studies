using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.BL.Map
{
    public interface IBomber : IPlayer2D
    {
        event EventHandler<IPosition2D> Moved;
    }
}
