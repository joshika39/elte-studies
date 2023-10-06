using Bomber.BL.Map;
using Infrastructure.Repositories;

namespace Bomber.BL.Repositories
{
    public interface IRouter
    {
        IRepository<IMapLayoutDraft> DraftLayouts { get; }
        IRepository<IMapLayout> MapLayouts { get; }
    }
}
