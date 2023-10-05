using Bomber.BL.Map;
using GameFramework.Map.MapObject;
using UiFramework.Forms;

namespace Bomber.UI.Forms.MapGenerator
{
    public interface IMapGeneratorWindowPresenter : IWindowPresenter
    {
        IMapLayoutDraft SelectedDraft { get; }
        IEnumerable<IMapObject2D> ReloadDraftLayout();
    }
}
