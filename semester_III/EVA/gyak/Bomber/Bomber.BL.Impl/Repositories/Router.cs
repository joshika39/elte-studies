using Bomber.BL.Impl.DomainModels;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using Bomber.BL.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Factories;

namespace Bomber.BL.Impl.Repositories
{
    public class Router : IRouter
    {
        public IRepository<IDraftLayoutModel> DraftLayouts { get; }
        public IRepository<IMapLayout> MapLayouts { get; }

        public Router(IRepositoryFactory repositoryFactory)
        {
            DraftLayouts = repositoryFactory.CreateJsonRepository<IDraftLayoutModel, DraftLayoutModel>("drafts");
            MapLayouts = repositoryFactory.CreateJsonRepository<IMapLayout, MapLayout>("layouts");
        }
    }
}
