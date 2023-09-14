using Labyrinth.UI.Forms.View.Main;
using Labyrinth.UI.Forms.View.Main._Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth.UI.Forms.Core
{
    public class UiBootstrapper
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddTransient<IMainWindow, MainWindow>();
            collection.AddTransient<IMainWindowPresenter, MainWindowPresenter>();
        }
    }
}
