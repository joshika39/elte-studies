using Bomber.Main;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Main
{
    public class MainWindowPresenter : AMainWindowModel, IMainWindowPresenter
    {
        private readonly IServiceProvider _provider;
        public MainWindowPresenter(IServiceProvider provider, IConfigurationService2D configurationService) : base(provider, configurationService)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        
        public void OpenMapGenerator()
        {
            var generatorWindow = _provider.GetRequiredService<IMapGeneratorWindow>();
            generatorWindow.ShowOnTop();
        }
        
    }
}
