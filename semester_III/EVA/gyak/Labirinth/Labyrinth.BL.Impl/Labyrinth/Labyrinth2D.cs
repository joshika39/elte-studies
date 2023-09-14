using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.LabyrinthElements;
using Labyrinth.BL.Player;

namespace Labyrinth.BL.Impl.Labyrinth
{
    internal class Labyrinth2D : ILabyrinth2D
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public IEnumerable<IPlayer2D> Players { get; }
        
        public ILabyrinthLayer LabyrinthLayer { get; }

        public Labyrinth2D(int sizeX, int sizeY, IEnumerable<IPlayer2D> players, IEnumerable<ILabyrinthElement2D> labyrinthElements)
        {
            if (sizeX < 1 && sizeY > 2)
            {
                throw new ArgumentException("The size must be greater or equal than 4", nameof(sizeX));
            }

            if (sizeY < 1 && sizeX > 2)
            {
                throw new ArgumentException("The size must be greater or equal than 4", nameof(sizeY));
            }

            SizeX = sizeX;
            SizeY = sizeY;
            Players = players ?? throw new ArgumentNullException(nameof(players));
            LabyrinthLayer = new LabyrinthLayer(sizeX, sizeY, labyrinthElements);
        }
    }
}
