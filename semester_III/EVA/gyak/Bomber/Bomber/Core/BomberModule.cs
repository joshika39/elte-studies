using Bomber.Main;
using Bomber.MapGenerator;
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
            
            collection.AddTransient<IMapGeneratorWindow, MapGeneratorWindow>();
            collection.AddTransient<IMapGeneratorWindowPresenter, MapGeneratorWindowPresenter>();
        }
    }
}
