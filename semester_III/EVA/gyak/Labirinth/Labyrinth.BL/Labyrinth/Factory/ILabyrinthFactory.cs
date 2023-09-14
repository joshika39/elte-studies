using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Labyrinth.Factory
{
    public interface ILabyrinthFactory
    {
        ILabyrinth2D CreateDefaultLabyrinth(IEnumerable<ILabyrinthElement2D> labyrinthElements);
    }
}
