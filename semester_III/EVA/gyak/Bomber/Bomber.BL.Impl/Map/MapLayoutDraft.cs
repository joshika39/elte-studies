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
            _layoutPath = Path.Join(settings.ConfigurationFolder, "draftLayouts", Id + ".txt");
            MapObjects = GetMapObjects();
            CreateFileAndDirectory();
        }

        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        [JsonIgnore]
        public IEnumerable<IPlaceHolder> MapObjects { get; }
        public void SaveLayout()
        {
            var streamWriter = new StreamWriter(_layoutPath);
            for (var i = 0; i < RowCount; i++)
            {
                var stringBuilder = new StringBuilder();
                for (var j = 0; j < ColumnCount; j++)
                {
                    var tile = MapObjects.FirstOrDefault(t => t.Position.X == i && t.Position.Y == j);
                    stringBuilder.Append($"{Constants.TileTypeToInt(tile?.Type ?? TileType.Ground)} ");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                streamWriter.WriteLine(stringBuilder.ToString());
            }
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Name) ? Id.ToString() : Name;
        }

        private IEnumerable<IPlaceHolder> GetMapObjects()
        {
            CreateFileAndDirectory();
            var content = _reader.ReadAllLines<int>(new StreamReader(_layoutPath), int.TryParse, ' ', '\t').ToArray();
            var array = new IPlaceHolder[RowCount * ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var j = 0; j < ColumnCount; j++)
                {
                    var pos = _positionFactory.CreatePosition(i, j);
                    var success = false;
                    if (i < content.Length)
                    {
                        var row = content[i].ToArray();

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
            return array;
        }
        
        private void CreateFileAndDirectory()
        {
            var directory = Path.GetDirectoryName(_layoutPath) ?? "";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(_layoutPath)) return;
            
            File.Create(_layoutPath).Close();
        }
    }
}
