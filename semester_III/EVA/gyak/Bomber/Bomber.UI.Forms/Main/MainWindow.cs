using Bomber.Map;
using Bomber.Objects;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Map.MapObject;
using Infrastructure.IO;
using Microsoft.Extensions.DependencyInjection;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public IMainWindowPresenter Presenter { get; }

        private readonly IConfigurationService _service;
        private readonly IPositionFactory _factory;
        private readonly IServiceProvider _provider;
        public MainWindow(IConfigurationService service, IPositionFactory factory, IServiceProvider provider, IMainWindowPresenter presenter)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _provider = provider;
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            using (var scope = _provider.CreateScope())
            {
                var reader = scope.ServiceProvider.GetRequiredService<IReader>();
                using var stream = new StreamReader(@"MapLayouts\layout-1.txt");
                var mapLayout = reader.ReadAllLines<int>(stream, int.TryParse, ' ').ToList();
                for (var i = 0; i < mapLayout.Count; i++)
                {
                    var row = mapLayout[i].ToList();
                    for (var j = 0; j < row.Count; j++)
                    {
                        var value = row[j];
                        var position = _factory.CreatePosition(i, j);
                        if (!Enum.TryParse(value.ToString(), out TileType type)) continue;
                        
                        IMapObject2D tile = type switch
                        {
                            TileType.Ground => new GroundTile(position, _service),
                            TileType.Wall => new WallTile(position, _service),
                            TileType.Hole => throw new NotImplementedException(),
                            _ => throw new ArgumentException($"Unknown tile type: {value}")
                        }; 
                        bomberMap.Controls.Add((Control)tile);
                    }
                }
            }
        }

        public DialogResult ShowOnTop()
        {
            var result = ShowDialog();

            switch (result)
            {
                case System.Windows.Forms.DialogResult.Cancel:
                case System.Windows.Forms.DialogResult.Abort:
                    return UiFramework.Shared.DialogResult.Cancelled;
                case System.Windows.Forms.DialogResult.Yes:
                case System.Windows.Forms.DialogResult.OK:
                    return UiFramework.Shared.DialogResult.Resolved;
                case System.Windows.Forms.DialogResult.None:
                case System.Windows.Forms.DialogResult.Retry:
                case System.Windows.Forms.DialogResult.Ignore:
                case System.Windows.Forms.DialogResult.No:
                case System.Windows.Forms.DialogResult.TryAgain:
                case System.Windows.Forms.DialogResult.Continue:
                default:
                    throw new InvalidOperationException("Unsupported dialog result!");
            }
        }

        private void openMapGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OpenMapGenerator();
        }
    }
}
