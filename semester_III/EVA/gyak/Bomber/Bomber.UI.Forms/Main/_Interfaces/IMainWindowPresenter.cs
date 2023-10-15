using Bomber.UI.Shared.Views;
using UiFramework.Forms;
using UiFramework.Shared;

namespace Bomber.Main
{
    public interface IMainWindowPresenter : IWindowPresenter, IMainWindowModel
    {
        void OpenMapGenerator();
    }
}
