using Bomber.BL.Map;
using GameFramework.Map;
using UiFramework.Forms;

namespace Bomber.UI.Forms.Objects.Player._Interfaces
{
    public interface IPlayerViewPresenter : IViewPresenter
    {
        void Move(MoveDirection moveDirection, IMap2D map);
    }
}
