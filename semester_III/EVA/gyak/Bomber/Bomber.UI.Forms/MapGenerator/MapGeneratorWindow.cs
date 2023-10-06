using Bomber.MapGenerator;
using Bomber.UI.Forms.MapGenerator;
using GameFramework.Map.MapObject;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms.MapGenerator
{
    public partial class MapGeneratorWindow : Form, IMapGeneratorWindow
    {
        private int _selectedLayoutWidth;
        private int _selectedLayoutHeight;
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

        private void OnWidthChanged(object sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDown) return;

            _selectedLayoutWidth = (int)numericUpDown.Value;
            numericUpDown.Value = (int)numericUpDown.Value;
            Presenter.SelectedDraft.SetCol(_selectedLayoutWidth);
            PopulatePanel(Presenter.ReloadDraftLayout());
        }

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDown) return;

            _selectedLayoutHeight = (int)numericUpDown.Value;
            numericUpDown.Value = (int)numericUpDown.Value;
            Presenter.SelectedDraft.SetRow(_selectedLayoutHeight);
            PopulatePanel(Presenter.ReloadDraftLayout());
        }

        private void PopulatePanel(IEnumerable<IMapObject2D> mapObjects)
        {
            widthValue.Value = Presenter.SelectedDraft.ColumnCount;
            heightValue.Value = Presenter.SelectedDraft.RowCount;
            layoutPanel.Controls.Clear();
            foreach (var mapObject in mapObjects)
            {
                if (mapObject is Control tile)
                {
                    layoutPanel.Controls.Add(tile);
                }
            }
        }
    }
}

