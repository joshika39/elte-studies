using Bomber.MapGenerator;
using UiFramework.Forms.Impl;
using UiFramework.Shared;

namespace Bomber.Main
{
    public class MainWindowPresenter : IMainWindowPresenter
    {
        private readonly IMapGeneratorWindow _mapGeneratorWindow;
        
        public MainWindowPresenter(IMapGeneratorWindow mapGeneratorWindow)
        {
            _mapGeneratorWindow = mapGeneratorWindow ?? throw new ArgumentNullException(nameof(mapGeneratorWindow));
        }
        
        public void OpenMapGenerator()
        {
            _mapGeneratorWindow.Show();
        }
    }
}
