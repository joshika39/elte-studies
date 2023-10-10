using System.ComponentModel;

namespace Bomber.UI.Forms.MapGenerator
{
    partial class MapGeneratorWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            generateButton = new Button();
            label1 = new Label();
            layoutPanel = new Panel();
            panel2 = new Panel();
            label6 = new Label();
            draftComboBox = new ComboBox();
            heightValue = new NumericUpDown();
            widthValue = new NumericUpDown();
            draftButton = new Button();
            cancelButton = new Button();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            descBox = new RichTextBox();
            label2 = new Label();
            backgroundWorker1 = new BackgroundWorker();
            draftName = new TextBox();
            label7 = new Label();
            panel2.SuspendLayout();
            ((ISupportInitialize)heightValue).BeginInit();
            ((ISupportInitialize)widthValue).BeginInit();
            SuspendLayout();
            // 
            // generateButton
            // 
            generateButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            generateButton.Location = new Point(226, 414);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 0;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(235, 45);
            label1.TabIndex = 1;
            label1.Text = "Map Generator";
            // 
            // layoutPanel
            // 
            layoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            layoutPanel.AutoSize = true;
            layoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutPanel.Location = new Point(12, 57);
            layoutPanel.Name = "layoutPanel";
            layoutPanel.Size = new Size(0, 0);
            layoutPanel.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label7);
            panel2.Controls.Add(draftName);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(draftComboBox);
            panel2.Controls.Add(heightValue);
            panel2.Controls.Add(widthValue);
            panel2.Controls.Add(draftButton);
            panel2.Controls.Add(cancelButton);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(generateButton);
            panel2.Controls.Add(descBox);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(488, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(312, 450);
            panel2.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(39, 20);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 12;
            label6.Text = "Pending drafts";
            // 
            // draftComboBox
            // 
            draftComboBox.FormattingEnabled = true;
            draftComboBox.Location = new Point(146, 16);
            draftComboBox.Margin = new Padding(3, 2, 3, 2);
            draftComboBox.Name = "draftComboBox";
            draftComboBox.Size = new Size(133, 23);
            draftComboBox.TabIndex = 11;
            draftComboBox.SelectedIndexChanged += OnSelectionChanged;
            // 
            // heightValue
            // 
            heightValue.Location = new Point(54, 164);
            heightValue.Name = "heightValue";
            heightValue.Size = new Size(239, 23);
            heightValue.TabIndex = 10;
            heightValue.ValueChanged += OnHeightChanged;
            // 
            // widthValue
            // 
            widthValue.Location = new Point(54, 136);
            widthValue.Name = "widthValue";
            widthValue.Size = new Size(239, 23);
            widthValue.TabIndex = 9;
            widthValue.ValueChanged += OnWidthChanged;
            // 
            // draftButton
            // 
            draftButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            draftButton.Location = new Point(8, 414);
            draftButton.Name = "draftButton";
            draftButton.Size = new Size(95, 23);
            draftButton.TabIndex = 8;
            draftButton.Text = "Save as Draft";
            draftButton.UseVisualStyleBackColor = true;
            draftButton.Click += OnSaveAsDraftClicked;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom;
            cancelButton.Location = new Point(146, 414);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(9, 209);
            label5.Name = "label5";
            label5.Size = new Size(96, 15);
            label5.TabIndex = 6;
            label5.Text = "Map description";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(4, 167);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 5;
            label4.Text = "Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(7, 140);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 4;
            label3.Text = "Width";
            // 
            // descBox
            // 
            descBox.Anchor = AnchorStyles.Right;
            descBox.Location = new Point(8, 232);
            descBox.Name = "descBox";
            descBox.Size = new Size(286, 159);
            descBox.TabIndex = 3;
            descBox.Text = "";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            label2.Location = new Point(7, 56);
            label2.Name = "label2";
            label2.Padding = new Padding(5);
            label2.Size = new Size(294, 35);
            label2.TabIndex = 0;
            label2.Text = "Properties";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // draftName
            // 
            draftName.Location = new Point(54, 107);
            draftName.Name = "draftName";
            draftName.Size = new Size(239, 23);
            draftName.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(6, 112);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 14;
            label7.Text = "Name";
            // 
            // MapGeneratorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(layoutPanel);
            Controls.Add(label1);
            Name = "MapGeneratorWindow";
            Text = "MapGeneratorWindow";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)heightValue).EndInit();
            ((ISupportInitialize)widthValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button generateButton;
        private Label label1;
        private Panel layoutPanel;
        private Panel panel2;
        private Label label2;
        private BackgroundWorker backgroundWorker1;
        private Label label5;
        private Label label4;
        private Label label3;
        private RichTextBox descBox;
        private Button cancelButton;
        private Button draftButton;
        private NumericUpDown heightValue;
        private NumericUpDown widthValue;
        private Label label6;
        private ComboBox draftComboBox;
        private Label label7;
        private TextBox draftName;
    }
}

