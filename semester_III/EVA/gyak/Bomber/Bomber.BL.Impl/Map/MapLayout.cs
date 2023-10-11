using System.Text;
using Bomber.BL.Map;
using Bomber.BL.Tiles.Factories;
using GameFramework.Map.MapObject;
using Implementation.Repositories;
using Infrastructure.Configuration;
using Infrastructure.IO;

namespace Bomber.BL.Impl.Map
{
    public class MapLayout : IMapLayout
    {
        private readonly IConfigurationQuery _query;
        private readonly ITileFactory _tileFactory;
        private readonly IReader _reader;
        private string _mapDataBase64;
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        
        public IEnumerable<IMapObject2D> MapObjects { get; }

        public MapLayout(IConfigurationQuery query, ITileFactory tileFactory, IReader reader)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _tileFactory = tileFactory ?? throw new ArgumentNullException(nameof(tileFactory));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            Name = _query.GetStringAttribute("name") ?? throw new ArgumentNullException(nameof(query));
            Description = _query.GetStringAttribute("description") ?? throw new ArgumentNullException(nameof(query));
            ColumnCount = _query.GetIntAttribute("row") ?? throw new ArgumentNullException(nameof(query));
            RowCount = _query.GetIntAttribute("col") ?? throw new ArgumentNullException(nameof(query));
            _mapDataBase64 = _query.GetStringAttribute("data") ?? throw new ArgumentNullException(nameof(query));
        }

        private IEnumerable<IMapObject2D> ConvertDataToObjects()
        {
            var rawData = Encoding.UTF8.GetString(Convert.FromBase64String(_mapDataBase64));
            var test = _reader.ReadAllLines("asdasd");
            return new List<IMapObject2D>();
        }
    }
}
