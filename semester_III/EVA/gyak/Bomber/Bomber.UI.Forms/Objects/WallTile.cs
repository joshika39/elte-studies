using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.Objects
{
    public partial class WallTile : UserControl, IMapObject2D
    {
        public IPosition2D Position { get; }

        public WallTile(IPosition2D position, IConfigurationService configurationService)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.X * configurationService.Dimension;
            Left = position.Y * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Gray;
        }
    }
}

