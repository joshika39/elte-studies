using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Labyrinth;

public interface ILabyrinthLayer
{
    IEnumerable<IEnumerable<ILabyrinthElement2D>> Layer { get; }
}