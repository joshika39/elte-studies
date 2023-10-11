using System.Text;
using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using Bomber.BL.Tiles;
using Bomber.BL.Tiles.Factories;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using Infrastructure.Application;
using Infrastructure.IO;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ArgumentNullException = System.ArgumentNullException;

namespace Bomber.BL.Impl.Map
{
    public class MapLayoutDraft : IMapLayoutDraft
    {
        private readonly IReader _reader;
        private readonly ITileFactory _tileFactory;
        private readonly IPositionFactory _positionFactory;
        private readonly IConfigurationService _configurationService;
        private readonly string _layoutPath;
        private int _columnCount;
        private int _rowCount;
        public string Name { get; set; }
        public Guid Id { get; }

        public MapLayoutDraft(IServiceProvider serviceProvider, IDraftLayoutModel model)
        {
            _reader = serviceProvider.GetRequiredService<IReader>();
            var settings = serviceProvider.GetRequiredService<IApplicationSettings>();
            _tileFactory = serviceProvider.GetRequiredService<ITileFactory>();
            _positionFactory = serviceProvider.GetRequiredService<IPositionFactory>();
            _configurationService = serviceProvider.GetRequiredService<IConfigurationService>();
            model = model ?? throw new ArgumentNullException(nameof(model));
            Description = model.Description;
            Name = model.Name;
            Id = model.Id;
            ColumnCount = model.ColumnCount;
            RowCount = model.RowCount;
            _layoutPath = Path.Join(settings.ConfigurationFolder, "layouts", "draftLayouts", Id + ".txt");
            Constants.CreateFileAndDirectory(_layoutPath);
            MapObjects = FirstLoad();
        }

        public string Description { get; set; }
        public int ColumnCount
        {
            get => _columnCount;
            set
            {
                _columnCount = value;
                UpdateLayout();
            }
        }
        public int RowCount
        {
            get => _rowCount;
            set
            {
                _rowCount = value;
                UpdateLayout();
            }
        }

        [JsonIgnore]
        public IEnumerable<IPlaceHolder> MapObjects { get; private set; }

        public void SaveLayout(IEnumerable<IPlaceHolder> newMapObjects)
        {
            var mapObjects = newMapObjects.ToArray();
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    var tile = mapObjects[i * ColumnCount + j];
                    stringBuilder.Append($"{Constants.TileTypeToInt(tile.Type)} ");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("\r\n");
            }
            File.WriteAllText(_layoutPath, stringBuilder.ToString());
            
            MapObjects = mapObjects;
        }
        
        public void UpdateLayout()
        {
            if (MapObjects == null)
            {
                return;
            }
            var oldValues = MapObjects.ToArray();
            var array = new IPlaceHolder[RowCount * ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    var pos = _positionFactory.CreatePosition(i, j);
                    if (oldValues is not null && i <= oldValues.Length / RowCount  && j <= oldValues.Length / ColumnCount && i * ColumnCount + j < oldValues.Length)
                    {
                        array[i * ColumnCount + j] = _tileFactory.CreatePlaceHolder(pos, _configurationService, oldValues[i * ColumnCount + j].Type);
                    }
                    else
                    {
                        array[i * ColumnCount + j] = _tileFactory.CreatePlaceHolder(pos, _configurationService);
                    }
                }
            }
            MapObjects = array;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Name) ? Id.ToString() : Name;
        }
        
        private IEnumerable<IPlaceHolder> FirstLoad()
        {
            using var streamReader = new StreamReader(_layoutPath);
            Constants.CreateFileAndDirectory(_layoutPath);
            var content = _reader.ReadAllLines<int>(streamReader, int.TryParse, ' ').ToArray();
            var array = new IPlaceHolder[RowCount * ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                var row = content[i].ToArray();
                for (var j = 0; j < ColumnCount; j++)
                {
                    var pos = _positionFactory.CreatePosition(i, j);
                    var success = false;
                    if (i < content.Length)
                    {
            
                        if (j < row.Length)
                        {
                            var type = Constants.IntToTileType(row[j]);
                            array[i * ColumnCount + j] = _tileFactory.CreatePlaceHolder(pos, _configurationService, type);
                            success = true;
                        }
                    }
                    if (success)
                    {
                        continue;
                    }
                    array[i * ColumnCount + j] = _tileFactory.CreatePlaceHolder(pos, _configurationService);
                }
            }
            streamReader.Close();
            return array;
        }
    }
}
