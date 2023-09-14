namespace Labyrinth.UI.Forms.Core
{
    public interface IWindow : IView
    {
        void Show();
        Form GetForm();
    }
}
