using Bomber.BL.Map;
using Bomber.BL.Repositories;
using Implementation.Repositories;

namespace Bomber.BL.Impl.Repositories
{
    public class MapLayoutRepository : AJsonRepository<IMapLayout>, IMapLayoutRepository
    {
        public MapLayoutRepository(string filePath) : base(filePath)
        { }
        public MapLayoutRepository(string directory, string repositoryKey) : base(directory, repositoryKey)
        { }
    }
}
