using UiFramework.Forms.Impl;
using UiFramework.Shared;

namespace Bomber.Main
{
    public class MainWindowPresenter : AWindowPresenter, IMainWindowPresenter
    {
        public MainWindowPresenter(IMainWindow window) : base(window)
        { }
        
        public override void Show()
        {
            throw new NotImplementedException();
        }
    }
}
