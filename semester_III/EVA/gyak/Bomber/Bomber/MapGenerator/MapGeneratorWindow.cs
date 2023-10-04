using UiFramework.Shared;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.MapGenerator
{
    public partial class MapGeneratorWindow : Form, IMapGeneratorWindow
    {
        public IMapGeneratorWindowPresenter Presenter { get; }

        public MapGeneratorWindow(IMapGeneratorWindowPresenter presenter)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            InitializeComponent();
        }

        public DialogResult ShowOnTop()
        {
            throw new NotImplementedException();
        }
    }
}

