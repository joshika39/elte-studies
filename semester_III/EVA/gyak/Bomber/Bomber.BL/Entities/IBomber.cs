using Bomber.UI.Shared.Entities;
using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomber : IPlayer2D
    {
        void PutBomb(IBombView bombView);
    }
}
