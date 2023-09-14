using Labyrinth.UI.Forms.View.Main._Interfaces;

namespace Labyrinth.UI.Forms.View.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            
            Map = new Panel();
            Map.Height = 400;
            Map.Width = 400;
            Map.Top = Height / 2 - Map.Height / 2;
            Map.Left = Width / 2  - Map.Width / 2;
            Map.BackColor = Color.Black;
            
            Controls.Add(Map);
            
            ResumeLayout(false);
        }

        public Form GetForm()
        {
            return this;
        }

        public Panel Map { get; }
    }
}

