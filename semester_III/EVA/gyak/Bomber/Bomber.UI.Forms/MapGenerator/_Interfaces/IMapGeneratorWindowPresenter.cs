using Bomber.BL.Map;
using UiFramework.Forms;

namespace Bomber.UI.Forms.MapGenerator
{
    public interface IMapGeneratorWindowPresenter : IWindowPresenter
    {
        IMapLayoutDraft SelectedDraft { get; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
    }
}
