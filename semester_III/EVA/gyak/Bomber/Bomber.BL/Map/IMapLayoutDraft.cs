using Infrastructure.Repositories;

namespace Bomber.BL.Map
{
    public interface IMapLayoutDraft : IMapLayout
    {
        void SetRow(int rowCount);
        void SetCol(int colCount);
        void SetDesc(string description);
    }
}
