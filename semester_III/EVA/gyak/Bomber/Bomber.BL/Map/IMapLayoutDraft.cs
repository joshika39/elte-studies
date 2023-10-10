using Bomber.BL.Tiles;
using GameFramework.Map.MapObject;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Bomber.BL.Map
{
    public interface IMapLayoutDraft : IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
        int ColumnCount { get; set; }
        int RowCount { get; set; }
        
        [JsonIgnore]
        IEnumerable<IPlaceHolder> MapObjects { get; }
        void SaveLayout();
    }
}
