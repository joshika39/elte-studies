using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.Objects;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.MapGenerator
{
    public class MapGeneratorWindowPresenter : IMapGeneratorWindowPresenter
    {
        private readonly IConfigurationService _service;
        private readonly IPositionFactory _factory;
        public IMapLayoutDraft SelectedDraft { get; } = new MapLayoutDraft();

        public MapGeneratorWindowPresenter(IConfigurationService service, IPositionFactory factory)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        
        public IEnumerable<IMapObject2D> ReloadDraftLayout()
        {
            // while (SelectedDraft.ColumnCount > _currentLayout.Count)
            // {
            //     _currentLayout.Add(new List<IMapObject2D>());
            // }
            //
            // for (var i = 0; i < _currentLayout.Count; i++)
            // {
            //     var row = _currentLayout[i];
            //     while (SelectedDraft.ColumnCount > row.Count)
            //     {
            //         var pos = _factory.CreatePosition(i, row.Count);
            //         row.Add(new GroundTile(pos, _service));
            //     }
            // }

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
