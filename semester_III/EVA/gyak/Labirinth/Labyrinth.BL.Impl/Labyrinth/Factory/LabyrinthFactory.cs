using Labyrinth.BL.Impl.Base;
using Labyrinth.BL.Impl.Player;
using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.Labyrinth.Factory;
using Labyrinth.BL.LabyrinthElements;
using Labyrinth.BL.Player;

namespace Labyrinth.BL.Impl.Labyrinth.Factory
{
    internal class LabyrinthFactory : ILabyrinthFactory
    {
        public ILabyrinth2D CreateDefaultLabyrinth(IEnumerable<ILabyrinthElement2D> labyrinthElements)
        {
            var player = new Player2D("Joshua", "test@alma.com", new Position2D(0, 0));
            return new Labyrinth2D(4, 4, new List<IPlayer2D> { player }, labyrinthElements);
        }
    }
}