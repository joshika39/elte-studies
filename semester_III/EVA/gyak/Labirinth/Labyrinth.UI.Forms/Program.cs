using Labyrinth.BL.Impl.Framework;
using Labyrinth.UI.Forms;
using Labyrinth.UI.Forms.Core;
using Labyrinth.UI.Forms.View.Main;
using Labyrinth.UI.Forms.View.Main._Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Labirinth;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        var modules = LoadModules();
        var mainPresenter = modules.GetService<IMainWindowPresenter>();
        var form = mainPresenter.Window.GetForm(); 
        Application.Run(form);
    }

    static IServiceProvider LoadModules()
    {
        var modules = new ServiceCollection();
        
        var labyrinthFramework = new LabyrinthFramework();
        var uiModules = new UiBootstrapper();
    
        labyrinthFramework.LoadModules(modules);
        uiModules.LoadModules(modules);
        
        return modules.BuildServiceProvider();
    }
}
