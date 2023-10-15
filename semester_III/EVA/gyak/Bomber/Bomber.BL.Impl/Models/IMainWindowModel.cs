using Bomber.BL.Map;

namespace Bomber.UI.Shared.Views
{
    public interface IMainWindowModel
    {
        IBomberMap OpenMap(string mapFileName);
    }
}
