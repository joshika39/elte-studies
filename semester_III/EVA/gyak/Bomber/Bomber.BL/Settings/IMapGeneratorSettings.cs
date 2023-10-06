using Bomber.BL.Map;

namespace Bomber.BL.Settings
{
    public interface IMapGeneratorSettings
    {
        Task<IMapLayoutDraft> SelectedDraft { get; }
    }
}
