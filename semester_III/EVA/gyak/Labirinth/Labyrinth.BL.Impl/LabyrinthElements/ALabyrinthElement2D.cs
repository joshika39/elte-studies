using Labyrinth.BL.Base;
using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Impl.LabyrinthElements
{
    internal abstract class ALabyrinthElement2D : ILabyrinthElement2D
    {
        public IPosition2D Position { get; }

        protected ALabyrinthElement2D(IPosition2D position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}
