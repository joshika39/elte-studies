using Labyrinth.BL.LabyrinthElements;
using Labyrinth.UI.Forms.Core;

namespace Labyrinth.UI.Forms.View.Floor;

public class FloorViewPresenter : IViewPresenter
{
    private readonly ILabyrinthElement2D _floor;
    private readonly FloorView _view;
    public UserControl View => _view;

    public FloorViewPresenter(ILabyrinthElement2D floor, FloorView view)
    {
        _floor = floor ?? throw new ArgumentNullException(nameof(floor));
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _view.Top = _floor.Position.X * 30;
        _view.Left = _floor.Position.Y * 30;
    }
}