using System.Collections.ObjectModel;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.BL.Repositories;
using Bomber.BL.Settings;
using Bomber.Objects;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Map.MapObject;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Factories;

namespace Bomber.UI.Forms.MapGenerator
{
    public class MapGeneratorWindowPresenter : IMapGeneratorWindowPresenter
    {
        private readonly IConfigurationService _service;
        private readonly IPositionFactory _factory;
        private readonly IRouter _repositoryRouter;
        private readonly IMapGeneratorSettings _mapGeneratorSettings;
        
        public IMapLayoutDraft SelectedDraft { get; }
        public IList<IMapLayoutDraft> Drafts => _repositoryRouter.DraftLayouts.GetAllEntities().ToList();

        public MapGeneratorWindowPresenter(IConfigurationService service, IPositionFactory factory, IRouter repositoryRouter, IMapGeneratorSettings mapGeneratorSettings)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _repositoryRouter = repositoryRouter ?? throw new ArgumentNullException(nameof(repositoryRouter));
            _mapGeneratorSettings = mapGeneratorSettings ?? throw new ArgumentNullException(nameof(mapGeneratorSettings));
            SelectedDraft = _mapGeneratorSettings.SelectedDraft;
        }
        
        public IEnumerable<IMapObject2D> ReloadDraftLayout()
        {
            var array = new IMapObject2D[SelectedDraft.RowCount * SelectedDraft.ColumnCount];
            for (var i = 0; i < SelectedDraft.RowCount; i++)
            {
                for (var j = 0; j < SelectedDraft.ColumnCount; j++)
                {
                    var pos = _factory.CreatePosition(i, j);
                    array[i * SelectedDraft.ColumnCount + j] = new GroundTile(pos, _service);
                }
            }
            return array;
        }
    }
}
