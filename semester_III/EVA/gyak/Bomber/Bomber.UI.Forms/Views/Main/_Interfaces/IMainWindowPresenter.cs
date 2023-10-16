using Bomber.BL.Impl.Models;
using Bomber.UI.Shared.Views;
using UiFramework.Forms;

namespace Bomber.UI.Forms.Main
{
    public interface IMainWindowPresenter : IWindowPresenter, IMainWindowModel
    {
        void OpenMapGenerator();
        IMainWindow View { get; }
    }
}
