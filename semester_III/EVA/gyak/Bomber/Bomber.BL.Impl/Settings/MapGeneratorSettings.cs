using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.BL.Repositories;
using Bomber.BL.Settings;
using Infrastructure.Application;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Factories;
using Infrastructure.Repositories;

namespace Bomber.BL.Impl.Settings
{
    public class MapGeneratorSettings : IMapGeneratorSettings
    {
        private readonly IConfigurationQuery _query;
        private readonly IRepository<IMapLayoutDraft> _draftsRepository;
        public IMapLayoutDraft SelectedDraft => GetSelectedDraftAsync();

        public MapGeneratorSettings(IApplicationSettings settings, IConfigurationQueryFactory configurationQueryFactory, IRouter router)
        {
            var path = Path.Join(settings.ConfigurationFolder, "generator-config.json");
            _query = configurationQueryFactory.CreateConfigurationQuery(path);
            _draftsRepository = router.DraftLayouts;

        }

        private IMapLayoutDraft GetSelectedDraftAsync()
        {
            var allEntities = _draftsRepository.GetAllEntities();

            var selectedId = _query.GetStringAttribute("selected-draft");
            if (selectedId == null)
            {
                return CreateNewSelectedDraft();
            }
            
            var parsed = Guid.Parse(selectedId);
            var selected = allEntities.FirstOrDefault(d => d.Id.Equals(parsed));
            return selected ?? CreateNewSelectedDraft();
        }

        private IMapLayoutDraft CreateNewSelectedDraft()
        {
            if (_draftsRepository.GetAllEntities().Any())
            {
                var layouts = _draftsRepository.GetAllEntities();
                var layout = layouts.First();
                _query.SetAttribute("selected-draft", layout.Id.ToString());
                return layout;
            }
            else
            {
                var layout = new MapLayoutDraft("");
                _ = _draftsRepository.Create(layout).SaveChanges();
                _query.SetAttribute("selected-draft", layout.Id.ToString());
                return layout; 
            }
        }
    }
}
