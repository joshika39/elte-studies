using Bomber.Main;
using Bomber.MapGenerator;
using Bomber.UI.Forms.MapGenerator;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.Core
{
    public class BomberModule
    {

        public void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<IMapGeneratorWindow, MapGeneratorWindow>();

            collection.AddSingleton<IMainWindowPresenter, MainWindowPresenter>();
            collection.AddSingleton<IMapGeneratorWindowPresenter, MapGeneratorWindowPresenter>();
        }
    }
}
