using Bomber.BL.Impl.DomainModels;
using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using Bomber.BL.Settings;
namespace Bomber.UI.Forms.MapGenerator
{
    public class MapGeneratorWindowPresenter : IMapGeneratorWindowPresenter
    {
        private readonly IMapGeneratorSettings _mapGeneratorSettings;

        public IMapLayoutDraft SelectedDraft
        {
            get => _mapGeneratorSettings.SelectedDraft;
            set => _mapGeneratorSettings.SelectedDraft = value;
        }
        
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

        public IDraftLayoutModel CreateDraft()
        {
            return _mapGeneratorSettings.CreateDraft();
        }
    }
}
