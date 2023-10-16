using Bomber.UI.Shared.Entities;
using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomber : IPlayer2D, IBomberEntity
    {
        void PutBomb(IBombView bombView);
    }
}
