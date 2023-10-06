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
        public Task<IMapLayoutDraft> SelectedDraft => GetSelectedDraftAsync();

        public MapGeneratorSettings(IApplicationSettings settings, IConfigurationQueryFactory configurationQueryFactory, IRouter router)
        {
            var path = Path.Join(settings.ConfigurationFolder, "generator-config.json");
            _query = configurationQueryFactory.CreateConfigurationQuery(path);
            _draftsRepository = router.DraftLayouts;
            
        }

        private async Task<IMapLayoutDraft> GetSelectedDraftAsync()
        {
            var selectedId = await _query.GetStringAttributeAsync("selected-draft");
            if (selectedId == null)
            {
                var layout = new MapLayoutDraft();
                await _draftsRepository.Create(layout).SaveChanges();
                return layout;
            }
            var parsed = Guid.Parse(selectedId);
            return _draftsRepository.GetAllEntities().Result.First(d => d.Id.Equals(parsed));
        }
    }
}
