using Labyrinth.UI.Forms.Core;
using Labyrinth.UI.Forms.View.Main._Interfaces;

namespace Labyrinth.UI.Forms.View.Main
{
    public class MainWindowPresenter : IMainWindowPresenter
    {
        public IWindow Window { get; }
        
        public MainWindowPresenter(IMainWindow window)
        {
            Window = window ?? throw new ArgumentNullException(nameof(window));

        }
    }
}
