﻿using Bomber.BL.Map;
using Implementation.Repositories;

namespace Bomber.BL.Impl.Map
{
    public class MapLayout : AEntity, IMapLayout
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
    }
}
