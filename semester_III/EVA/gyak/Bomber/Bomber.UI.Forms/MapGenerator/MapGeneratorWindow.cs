using Bomber.BL.Map;
using Bomber.MapGenerator;
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

            foreach (var draft in Presenter.Drafts)
            {
                draftComboBox.Items.Add(draft);
            }

            var selected = Presenter.SelectedDraft;
            var item = Presenter.Drafts.First(d => d.Id.Equals(selected.Id));
            draftComboBox.SelectedIndex = draftComboBox.Items.IndexOf(item);
            draftName.Text = selected.Name;
            descBox.Text = selected.Description;
            _selectedLayoutWidth = selected.ColumnCount;
            _selectedLayoutHeight = selected.RowCount;
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
            Presenter.SelectedDraft.ColumnCount = _selectedLayoutWidth;
            PopulatePanel(Presenter.SelectedDraft.MapObjects);
        }

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDown) return;

            _selectedLayoutHeight = (int)numericUpDown.Value;
            numericUpDown.Value = (int)numericUpDown.Value;
            Presenter.SelectedDraft.RowCount = _selectedLayoutHeight;
            PopulatePanel(Presenter.SelectedDraft.MapObjects);
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

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (sender is not ComboBox comboBox) return;

            if (comboBox.SelectedItem is not IMapLayoutDraft draft) return;

            widthValue.Value = draft.ColumnCount;
            heightValue.Value = draft.RowCount;
        }

        private void OnSaveAsDraftClicked(object sender, EventArgs e)
        {
            if (draftComboBox.SelectedItem is not IMapLayoutDraft draft) return;
            draft.ColumnCount = _selectedLayoutWidth;
            draft.RowCount = _selectedLayoutHeight;
            draft.Name = draftName.Text;
            draft.Description = descBox.Text;
            Presenter.UpdateDraft(draft);
        }
    }
}

