using UiFramework.Forms.Impl;
using UiFramework.Shared;

namespace Bomber.Main
{
    public class MainWindowPresenter : AWindowPresenter, IMainWindowPresenter
    {
        private MainWindow _mainWindow;
        public MainWindowPresenter(IMainWindow window) : base(window)
        {
            if (Window is MainWindow mainWindow)
            {
                _mainWindow = mainWindow;
            }
            else
            {
                throw new ArgumentException($"{nameof(_mainWindow)} is a wrong window type");
            }
        }
    }
}
