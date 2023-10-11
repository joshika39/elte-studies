using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using UiFramework.Forms;

namespace Bomber.UI.Forms.MapGenerator
{
    public interface IMapGeneratorWindowPresenter : IWindowPresenter
    {
        IMapLayoutDraft SelectedDraft { get; set; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
        IDraftLayoutModel CreateDraft();
    }
}
