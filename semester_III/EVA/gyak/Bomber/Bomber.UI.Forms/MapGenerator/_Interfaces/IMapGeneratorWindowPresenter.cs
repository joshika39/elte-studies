using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using GameFramework.Map.MapObject;
using UiFramework.Forms;

namespace Bomber.UI.Forms.MapGenerator
{
    public interface IMapGeneratorWindowPresenter : IWindowPresenter
    {
        IMapLayoutDraft SelectedDraft { get; }
        IList<IMapLayoutDraft> Drafts { get; }
        IEnumerable<IMapObject2D> ReloadDraftLayout();
    }
}
