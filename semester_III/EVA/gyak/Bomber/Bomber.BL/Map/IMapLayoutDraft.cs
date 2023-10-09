using Infrastructure.Repositories;

namespace Bomber.BL.Map
{
    public interface IMapLayoutDraft : IEntity
    {
        string Description { get; set; }
        int ColumnCount { get; set; }
        int RowCount { get; set; }
    }
}
