using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
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
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "BoB files (*.bob)|*.bob";
            openDialog.InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps");
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = openDialog.FileName;
                var map = new MapLayout(path, _provider);
                foreach (var mapMapObject in map.MapObjects)
                {
                    if (mapMapObject is Control control)
                    {
                        bomberMap.Controls.Add(control);
                    }
                }
            }
        }
    }
}
