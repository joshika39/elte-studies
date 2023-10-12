﻿using Bomber.BL.Impl;
using Bomber.BL.Map;
using Bomber.BL.Tiles;
using Bomber.MapGenerator;
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
            PopulatePanel(Presenter.SelectedDraft.MapObjects);
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

        private void PopulatePanel(IEnumerable<IPlaceHolder> mapObjects)
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
            draftName.Text = draft.Name;
            descBox.Text = draft.Description;
            Presenter.SelectedDraft = draft;
            PopulatePanel(Presenter.SelectedDraft.MapObjects);
        }

        private void OnSaveAsDraftClicked(object sender, EventArgs e)
        {
            Presenter.SelectedDraft.ColumnCount = _selectedLayoutWidth;
            Presenter.SelectedDraft.RowCount = _selectedLayoutHeight;
            Presenter.SelectedDraft.Name = draftName.Text;
            Presenter.SelectedDraft.Description = descBox.Text;
            Presenter.UpdateDraft(Presenter.SelectedDraft);
        }

        private void OnGenerateClick(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "BoB files (*.bob)|*.bob";
            dialog.OverwritePrompt = true;
            dialog.Title = "Select a file";
            var document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var folder = Path.Join(document, "joshika39", "Bomber", "maps");
            Constants.CreateDirectory(@$"{folder}\");
            dialog.InitialDirectory = folder;
            var result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            foreach (var mapItem in draftComboBox.Items)
            {
                if (mapItem is IMapLayoutDraft mapLayoutDraft)
                {
                    if (!mapLayoutDraft.Id.Equals(Presenter.SelectedDraft.Id)) continue;
                    
                    draftComboBox.Items.Remove(mapItem);
                    if (draftComboBox.Items.Count > 0 && draftComboBox.Items[0] is IMapLayoutDraft selected)
                    {
                        Presenter.SelectedDraft = selected;
                        draftName.Text = selected.Name;
                        descBox.Text = selected.Description;
                        _selectedLayoutWidth = selected.ColumnCount;
                        _selectedLayoutHeight = selected.RowCount;
                        PopulatePanel(Presenter.SelectedDraft.MapObjects);
                    }
                    else
                    {
                        draftName.Text = "";
                        descBox.Text = "";
                        _selectedLayoutWidth = 0;
                        _selectedLayoutHeight = 0;
                        PopulatePanel(new List<IPlaceHolder>());
                    }
                }
            }
            
            Presenter.GenerateMapFromDraft(Presenter.SelectedDraft, dialog.FileName);
        }

        private void OnNewClicked(object sender, EventArgs e)
        {
            Presenter.SelectedDraft = Presenter.CreateDraft();
            
            draftComboBox.Items.Add(Presenter.SelectedDraft);
            
            var selected = Presenter.SelectedDraft;
            var item = Presenter.Drafts.First(d => d.Id.Equals(selected.Id));
            draftComboBox.SelectedIndex = draftComboBox.Items.IndexOf(item);
            draftName.Text = selected.Name;
            descBox.Text = selected.Description;
            _selectedLayoutWidth = selected.ColumnCount;
            _selectedLayoutHeight = selected.RowCount;
            PopulatePanel(Presenter.SelectedDraft.MapObjects);
        }
    }
}

