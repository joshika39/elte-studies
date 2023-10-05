using Infrastructure.Repositories;

namespace Bomber.BL.Map
{
    public interface IMapLayout : IEntity
    {
        string Description { get; }
        int ColumnCount { get; }
        int RowCount { get; }
    }
}
