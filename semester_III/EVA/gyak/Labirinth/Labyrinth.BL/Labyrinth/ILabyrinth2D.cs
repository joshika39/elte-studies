using Labyrinth.BL.LabyrinthElements;
using Labyrinth.BL.Player;

namespace Labyrinth.BL.Labyrinth
{
    public interface ILabyrinth2D
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public IEnumerable<IPlayer2D> Players { get; }
        public ILabyrinthLayer LabyrinthLayer { get; }
    }
}
