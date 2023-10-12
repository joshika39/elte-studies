using Bomber.BL.Impl.Map;
using Bomber.BL.Impl.Player;
using Bomber.BL.Map;
using Bomber.UI.Forms.Objects;
using Bomber.UI.Forms.Objects.Player;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using GameFramework.Map.MapObject;
using Infrastructure.IO;
using Microsoft.Extensions.DependencyInjection;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public IMainWindowPresenter Presenter { get; }

        private readonly IConfigurationService2D _service;
        private readonly IPositionFactory _factory;
        private readonly IServiceProvider _provider;
        public MainWindow(IConfigurationService2D service, IPositionFactory factory, IServiceProvider provider, IMainWindowPresenter presenter)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            InitializeComponent();
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

        private void OnOpenMap(object sender, EventArgs e)
        {
            bomberMap.Controls.Clear();
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "BoB files (*.bob)|*.bob";
            openDialog.InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps");
            if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var path = openDialog.FileName;
            var mapLayout = new MapLayout(path, _provider);
            var map = new Map(
                mapLayout.ColumnCount,
                mapLayout.RowCount,
                new List<IUnit2D>(),
                mapLayout.MapObjects);
            
            _service.SetActiveMap(map);
            _service.GameIsRunning = true;

            var model = new PlayerModel(_factory.CreatePosition(3, 1), _factory, _service, "TestPlayer", "test@email.com");
            var player = new Player(model, _provider.GetRequiredService<IConfigurationService2D>());
            bomberMap.Controls.Add(player);

            foreach (var mapMapObject in mapLayout.MapObjects)
            {
                if (mapMapObject is Control control)
                {
                    var label = new Label();
                    label.Text = $"{mapMapObject.Position.X}, {mapMapObject.Position.Y}";
                    control.Controls.Add(label);
                    bomberMap.Controls.Add(control);
                }
            }
        }
    }
}
