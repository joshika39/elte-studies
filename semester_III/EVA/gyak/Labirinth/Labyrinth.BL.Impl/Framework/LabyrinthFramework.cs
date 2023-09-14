using Labyrinth.BL.Impl.Labyrinth.Factory;
using Labyrinth.BL.Impl.LabyrinthElements.Factory;
using Labyrinth.BL.Labyrinth.Factory;
using Labyrinth.BL.LabyrinthElements.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth.BL.Impl.Framework
{
    public class LabyrinthFramework
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddTransient<ILabyrinthElementFactory, LabyrinthElementFactory>();
            collection.AddTransient<ILabyrinthFactory, LabyrinthFactory>();
        }
    }
}
