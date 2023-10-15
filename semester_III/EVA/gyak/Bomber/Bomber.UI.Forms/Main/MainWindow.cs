using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.Main;
using Bomber.MapGenerator;
using Bomber.UI.Forms.Main._Interfaces;
using Bomber.UI.Forms.MapGenerator;
using Bomber.UI.Forms.Views.Entities;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Entities;
using Microsoft.Extensions.DependencyInjection;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public IMainWindowPresenter Presenter { get; }

        private IBomber? _player;

        private readonly IConfigurationService2D _service;
        private readonly IPositionFactory _factory;
        private readonly IServiceProvider _provider;

        public MainWindow(IConfigurationService2D service, IPositionFactory factory, IServiceProvider provider, IMainWindowPresenter presenter)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            KeyPreview = true;
            InitializeComponent();
            _token = new CancellationTokenSource();
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
            var generatorWindow = _provider.GetRequiredService<IMapGeneratorWindow>();
            generatorWindow.ShowOnTop();
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

            var map = Presenter.OpenMap(openDialog.FileName);

            var view = new PlayerView(_service);
            _player = new PlayerModel(view, _factory.CreatePosition(3, 1), _service, "TestPlayer", "test@email.com", _token.Token);
            bomberMap.Controls.Add(view);

            foreach (var mapMapObject in map.MapObjects)
            {
                if (mapMapObject is Control control)
                {
#if DEBUG
                    var label = new Label();
                    label.Text = $"{mapMapObject.Position.X}, {mapMapObject.Position.Y}";
                    control.Controls.Add(label);
#endif
                    bomberMap.Controls.Add(control);
                }
            }
        }

        private readonly CancellationTokenSource _token;

        private async void OnTestClick(object sender, EventArgs e)
        {
            var map = _service.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }

            var enemyView = new EnemyView(_service, map.NPCs.Count + 1);
            var enemy = new Enemy(enemyView, _service, _factory.CreatePosition(1, 4), _token.Token);
            bomberMap.Controls.Add(enemyView);
            map.NPCs.Add(enemy);
            await enemy.ExecuteAsync();
        }

        private void OnStopTestClick(object sender, EventArgs e)
        {
            _token.Cancel();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (!_service.GameIsRunning || _player is null)
            {
                return;
            }

            var map = _service.GetActiveMap<IBomberMap>();

            if (e.KeyCode == Keys.D)
            {
                map?.MoveUnit(_player, Move2D.Right);
            }

            if (e.KeyCode == Keys.A)
            {
                map?.MoveUnit(_player, Move2D.Left);
            }

            if (e.KeyCode == Keys.W)
            {
                map?.MoveUnit(_player, Move2D.Forward);
            }

            if (e.KeyCode == Keys.S)
            {
                map?.MoveUnit(_player, Move2D.Backward);
            }

            if (e.KeyCode == Keys.B)
            {
                var view = new BombView(_service);
                _player.PutBomb(view);
                bomberMap.Controls.Add(view);
            }
        }
    }
}
