using Bomber.Main;
using Bomber.MapGenerator;

namespace Bomber.UI.Forms.Main
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
            _mapGeneratorWindow.ShowOnTop();
        }
    }
}
