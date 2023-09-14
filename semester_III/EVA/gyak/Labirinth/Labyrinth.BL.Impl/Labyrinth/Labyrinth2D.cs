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
        public IEnumerable<IWall2D> Walls { get; }
        public IEnumerable<IFloor2D> Floors { get; }

        public Labyrinth2D(int sizeX, int sizeY, IEnumerable<IPlayer2D> players, IEnumerable<IWall2D> walls, IEnumerable<IFloor2D> floors)
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
            Walls = walls ?? throw new ArgumentNullException(nameof(walls));
            Floors = floors ?? throw new ArgumentNullException(nameof(floors));
        }
    }
}
