using GameFramework.Core;

namespace Bomber.UI.Shared.MapObjectView
{
    public interface IUnit2DView
    {
        void UpdatePosition(IPosition2D position);
        event EventHandler Load;
    }
}
