using Labyrinth.BL.Base;
using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Impl.LabyrinthElements
{
    internal class Floor2D : ALabyrinthElement2D, IFloor2D
    {

        public Floor2D(IPosition2D position) : base(position)
        { }
    }
}
