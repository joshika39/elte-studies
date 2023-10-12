using Bomber.BL.Map;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map;

namespace Bomber.BL.Player
{
    public interface IBomber : IPlayer2D
    {
        event EventHandler<IPosition2D> Moved;
        void Move(MoveDirection moveDirection, IMap2D map);
    }
}
