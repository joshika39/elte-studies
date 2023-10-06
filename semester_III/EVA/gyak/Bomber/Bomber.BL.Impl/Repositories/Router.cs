using Bomber.BL.Map;
using Bomber.BL.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Factories;

namespace Bomber.BL.Impl.Repositories
{
    public class Router : IRouter
    {
        public IRepository<IMapLayoutDraft> DraftLayouts { get; }
        public IRepository<IMapLayout> MapLayouts { get; }

        public Router(IRepositoryFactory repositoryFactory)
        {
            DraftLayouts = repositoryFactory.CreateJsonRepository<IMapLayoutDraft>("drafts");
            MapLayouts = repositoryFactory.CreateJsonRepository<IMapLayout>("layouts");
        }
    }
}
