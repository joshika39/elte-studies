using Labyrinth.BL.Impl.Base;
using Labyrinth.BL.LabyrinthElements;
using Labyrinth.BL.LabyrinthElements.Factory;

namespace Labyrinth.BL.Impl.LabyrinthElements.Factory
{
    internal class LabyrinthElementFactory : ILabyrinthElementFactory
    {
        public ILabyrinthElement2D CreateFloor(int top, int left)
        {
            return new Floor2D(new Position2D(top, left));
        }

        public ILabyrinthElement2D CreateWall(int top, int left)
        {
            return new Wall2D(new Position2D(top, left));
        }
    }
}
