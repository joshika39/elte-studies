using Labyrinth.UI.Forms.View.Main._Interfaces;

namespace Labyrinth.UI.Forms.View.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
        }
    }
}

