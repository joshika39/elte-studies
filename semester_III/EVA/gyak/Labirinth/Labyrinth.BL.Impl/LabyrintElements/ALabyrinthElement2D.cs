using Labyrinth.BL.Labyrinth;

namespace Labyrinth.BL.Impl.Labyrinth
{
    public abstract class ALabyrinthElement2D : ILabyrinthElement2D
    {
        public IPosition2D Position { get; }

        protected ALabyrinthElement2D(IPosition2D position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}
