using Bomber.BL.Impl;
using Bomber.Core;
using Bomber.Main;
using GameFramework.Impl.Core;
using Implementation.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var modules = LoadModules();
            var mainWindow = modules.GetRequiredService<IMainWindow>();

            if (mainWindow is MainWindow window)
            {
                Application.Run(window);
            }
        }

        private static IServiceProvider LoadModules()
        {
            var serviceProvider = new ServiceCollection();
            
            new CoreModule().LoadModules(serviceProvider, "Bomber");
            new GameModule().LoadModules(serviceProvider);
            new BusinessLogicModule().LoadModules(serviceProvider);
            new BomberModule().LoadModules(serviceProvider);

            return serviceProvider.BuildServiceProvider();
        }
    }
}
