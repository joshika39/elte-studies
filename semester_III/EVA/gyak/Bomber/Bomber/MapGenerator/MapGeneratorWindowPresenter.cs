using UiFramework.Forms.Impl;
using UiFramework.Shared;

namespace Bomber.MapGenerator
{
    public class MapGeneratorWindowPresenter : AWindowPresenter, IMapGeneratorWindowPresenter
    {
        private MapGeneratorWindow _mapGeneratorWindow;
        public MapGeneratorWindowPresenter(IWindow window) : base(window)
        {
            if (Window is MapGeneratorWindow mapGeneratorWindow)
            {
                _mapGeneratorWindow = mapGeneratorWindow;
            }
            else
            {
                throw new ArgumentException($"{nameof(_mapGeneratorWindow)} is a wrong window type");
            }
        }
    }
}
