using Bomber.BL.Map;

namespace Bomber.BL.Settings
{
    public interface IMapGeneratorSettings
    {
        IMapLayoutDraft SelectedDraft { get; }
        IEnumerable<IMapLayoutDraft> Drafts { get; }
        void UpdateDraft(IMapLayoutDraft draft);
    }
}
