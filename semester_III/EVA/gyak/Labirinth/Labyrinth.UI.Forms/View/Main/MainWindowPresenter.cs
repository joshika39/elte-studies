using Labyrinth.BL.LabyrinthElements;
using Labyrinth.BL.LabyrinthElements.Factory;
using Labyrinth.UI.Forms.Core;
using Labyrinth.UI.Forms.View.Floor;
using Labyrinth.UI.Forms.View.Main._Interfaces;

namespace Labyrinth.UI.Forms.View.Main
{
    public class MainWindowPresenter : IMainWindowPresenter
    {
        private readonly ILabyrinthElementFactory _labyrinthElementFactory;
        public IMainWindow Window { get; }
        
        public MainWindowPresenter(IMainWindow window, ILabyrinthElementFactory labyrinthElementFactory)
        {
            _labyrinthElementFactory = labyrinthElementFactory ?? throw new ArgumentNullException(nameof(labyrinthElementFactory));
            Window = window ?? throw new ArgumentNullException(nameof(window));
            InitFloor();
        }

        private void InitFloor()
        {
            var elements = new List<ILabyrinthElement2D>()
            {
                _labyrinthElementFactory.CreateWall(0, 0),
                _labyrinthElementFactory.CreateWall(0, 1),
                _labyrinthElementFactory.CreateWall(0, 2),
                _labyrinthElementFactory.CreateWall(0, 3),
                _labyrinthElementFactory.CreateFloor(1, 0),
                _labyrinthElementFactory.CreateFloor(1, 1),
                _labyrinthElementFactory.CreateFloor(1, 2),
                _labyrinthElementFactory.CreateWall(1, 3),
                _labyrinthElementFactory.CreateFloor(2, 0),
                _labyrinthElementFactory.CreateWall(2, 1),
                _labyrinthElementFactory.CreateFloor(2, 2),
                _labyrinthElementFactory.CreateWall(2, 3),
                _labyrinthElementFactory.CreateFloor(3, 0),
                _labyrinthElementFactory.CreateWall(3, 1),
                _labyrinthElementFactory.CreateFloor(3, 2),
                _labyrinthElementFactory.CreateWall(3, 3),
            };
            
            for (var i = 0; i < 10; i++)
            {
                var floor = new FloorView();
                floor.Top = 10;
                floor.Left = i * 30;
                Window.Map.Controls.Add(floor);
            }
        }
    }
}
