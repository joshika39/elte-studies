using Bomber.BL.Map;
using Bomber.BL.Map.DomainModels;
using Infrastructure.Repositories;

namespace Bomber.BL.Repositories
{
    public interface IRouter
    {
        IRepository<IDraftLayoutModel> DraftLayouts { get; }
        IRepository<IMapLayout> MapLayouts { get; }
    }
}
