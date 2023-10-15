using Bomber.Main;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;

namespace Bomber.UI.Forms.Main
{
    public class MainWindowPresenter : AMainWindowModel, IMainWindowPresenter
    {
        public MainWindowPresenter(IServiceProvider provider, IConfigurationService2D configurationService) : base(provider, configurationService)
        { }
        
        public void OpenMapGenerator()
        {
            throw new System.NotImplementedException();
        }
        
    }
}
