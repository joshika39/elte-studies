using Bomber.BL.Map.DomainModels;
using Implementation.Repositories;

namespace Bomber.BL.Impl.DomainModels
{
    public class DraftLayoutModel : AEntity, IDraftLayoutModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}
