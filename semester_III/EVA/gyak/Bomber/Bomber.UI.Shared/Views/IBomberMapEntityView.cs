using GameFramework.Core;

namespace Bomber.UI.Shared.Views
{
    public interface IBomberMapEntityView
    {
        void UpdatePosition(IPosition2D position);
        event EventHandler Load;
    }
}
