using Labyrinth.BL.Base;
using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Impl.LabyrinthElements
{
    internal class Wall2D : ALabyrinthElement2D, IWall2D
    {

        public Wall2D(IPosition2D position) : base(position)
        { }
    }
}
