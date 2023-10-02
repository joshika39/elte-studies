using Bomber.Main;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.Core
{
    public class BomberModule : IModule
    {

        public void LoadModules(IServiceCollection collection)
        {
            collection.AddTransient<IMainWindow, MainWindow>();
            collection.AddTransient<IMainWindowPresenter, MainWindowPresenter>();
        }
    }
}
