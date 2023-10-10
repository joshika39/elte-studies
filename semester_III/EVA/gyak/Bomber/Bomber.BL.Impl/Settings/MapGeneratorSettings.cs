using Bomber.BL.Impl.DomainModels;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using Bomber.BL.Repositories;
using Bomber.BL.Settings;
using Infrastructure.Application;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Factories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl.Settings
{
    public class MapGeneratorSettings : IMapGeneratorSettings
    {
        private readonly IServiceProvider _provider;
        private readonly IConfigurationQuery _query;
        private readonly IRepository<IDraftLayoutModel> _draftsRepository;
        public IMapLayoutDraft SelectedDraft => GetSelectedDraftAsync();
        public IEnumerable<IMapLayoutDraft> Drafts => GetDrafts();

        public MapGeneratorSettings(IServiceProvider provider, IConfigurationQueryFactory configurationQueryFactory, IRouter router)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            var path = Path.Join(_provider.GetRequiredService<IApplicationSettings>().ConfigurationFolder, "generator-config.json");
            _query = configurationQueryFactory.CreateConfigurationQuery(path);
            _draftsRepository = router.DraftLayouts;

        }
        
        public void UpdateDraft(IMapLayoutDraft draft)
        {
            var model = new DraftLayoutModel()
            {
                Id = draft.Id,
                Name = draft.Name,
                Description = draft.Description,
                ColumnCount = draft.ColumnCount,
                RowCount = draft.RowCount
            };
            _draftsRepository.Update(model).SaveChanges();
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
            return selected is null ? CreateNewSelectedDraft() : new MapLayoutDraft(_provider.GetRequiredService<IServiceProvider>(), selected);
        }

        private IMapLayoutDraft CreateNewSelectedDraft()
        {
            if (_draftsRepository.GetAllEntities().Any())
            {
                var layouts = _draftsRepository.GetAllEntities();
                var layout = layouts.First();
                _query.SetAttribute("selected-draft", layout.Id.ToString());
                return new MapLayoutDraft(_provider.GetRequiredService<IServiceProvider>(), layout);
            }
            else
            {
                var layoutModel = new DraftLayoutModel();
                var layout = new MapLayoutDraft(_provider.GetRequiredService<IServiceProvider>(), layoutModel);
                _ = _draftsRepository.Create(layoutModel).SaveChanges();
                _query.SetAttribute("selected-draft", layout.Id.ToString());
                return layout; 
            }
        }
        
        private IEnumerable<IMapLayoutDraft> GetDrafts()
        {
            var drafts = new List<IMapLayoutDraft>();
            foreach (var model in _draftsRepository.GetAllEntities())
            {
                drafts.Add(new MapLayoutDraft(_provider, model));
            }
            return drafts;
        }
    }
}
