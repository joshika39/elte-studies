using Labyrinth.BL.Floor;
using Labyrinth.BL.Player;
using Labyrinth.BL.Wall;

namespace Labyrinth.BL.Labyrinth
{
    public interface ILabyrinth2D
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public IEnumerable<IPlayer2D> Players { get; }
        public IEnumerable<IWall2D> Walls { get; }
        public IEnumerable<IFloor2D> Floors { get; }
    }
}
