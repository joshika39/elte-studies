using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.Player;
using Labyrinth.UI.Forms.View.Floor._Interfaces;

namespace Labyrinth.UI.Forms.View.Floor
{
    public partial class FloorView : UserControl, IFloorView, ILabyrinth2D
    {
        public FloorView()
        {
            InitializeComponent();
        }
        public int SizeX
        {
            get;
        }
        public int SizeY
        {
            get;
        }
        public IEnumerable<IPlayer2D> Players
        {
            get;
        }
        public ILabyrinthLayer LabyrinthLayer
        {
            get;
        }
    }
}

