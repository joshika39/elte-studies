using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.LabyrinthElements;

namespace Labyrinth.BL.Impl.Labyrinth;

public class LabyrinthLayer : ILabyrinthLayer
{
    public IEnumerable<IEnumerable<ILabyrinthElement2D>> Layer { get; }

    public LabyrinthLayer(int verticalElementCount, int horizontalElementCount, IEnumerable<ILabyrinthElement2D> elements)
    {
        if (elements == null) throw new ArgumentNullException(nameof(elements));
        var elementList = elements.ToArray();
        var layer = new List<IEnumerable<ILabyrinthElement2D>>();

        for (var i = 0; i < verticalElementCount; i++)
        {
            var row = new List<ILabyrinthElement2D>();
            for (var j = 0; j < horizontalElementCount; j++)
            {
                row.Add(elementList[j + i * horizontalElementCount]);
            }

            layer.Add(row);
        }

        Layer = layer;
    }
}