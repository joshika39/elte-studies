using Bomber.BL.Impl.DomainModels;
using Bomber.BL.Map;
using Bomber.BL.Settings;
namespace Bomber.UI.Forms.MapGenerator
{
    public class MapGeneratorWindowPresenter : IMapGeneratorWindowPresenter
    {
        private readonly IMapGeneratorSettings _mapGeneratorSettings;
        
        public IMapLayoutDraft SelectedDraft { get; }
        public IEnumerable<IMapLayoutDraft> Drafts => _mapGeneratorSettings.Drafts;

        public MapGeneratorWindowPresenter(IMapGeneratorSettings mapGeneratorSettings)
        {
            _mapGeneratorSettings = mapGeneratorSettings ?? throw new ArgumentNullException(nameof(mapGeneratorSettings));
            SelectedDraft = _mapGeneratorSettings.SelectedDraft;
        }
        
        public void UpdateDraft(IMapLayoutDraft draft)
        {
            _mapGeneratorSettings.UpdateDraft(draft);
            draft.SaveLayout(draft.MapObjects);
        }
    }
}
