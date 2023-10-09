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

        class DraftListItem
        {
            public IMapLayoutDraft Draft { get; }
            
            public DraftListItem(IMapLayoutDraft draft)
            {
                Draft = draft;
            }

            public override string ToString()
            {
                return Draft.Id.ToString();
            }
        }

        private IList<DraftListItem> _listItems;
        
        public MapGeneratorWindow(IMapGeneratorWindowPresenter presenter)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            InitializeComponent();
            _listItems = new List<DraftListItem>();
            foreach (var draft in Presenter.Drafts)
            {
                var draftItem = new DraftListItem(draft);
                _listItems.Add(draftItem);
                draftComboBox.Items.Add(draftItem);
            }
            var selected = Presenter.SelectedDraft;
            var item = _listItems.First(d => d.Draft.Id.Equals(selected.Id));
            var index = draftComboBox.Items.IndexOf(item);
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
            PopulatePanel(Presenter.ReloadDraftLayout());
        }

        private void OnHeightChanged(object sender, EventArgs e)
        {
            if (sender is not NumericUpDown numericUpDown) return;

            _selectedLayoutHeight = (int)numericUpDown.Value;
            numericUpDown.Value = (int)numericUpDown.Value;
            Presenter.SelectedDraft.RowCount = _selectedLayoutHeight;
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

