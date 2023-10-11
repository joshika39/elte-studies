using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;

namespace Bomber.BL.Settings
{
    public interface IMapGeneratorSettings
    {
        IMapLayoutDraft SelectedDraft { get; set; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
        IDraftLayoutModel CreateDraft();
    }
}
