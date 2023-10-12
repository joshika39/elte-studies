using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Objects
{
    public partial class WallTile : UserControl, IMapObject2D
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotImplementedException();
        }
        public IPosition2D Position { get; }
        public bool IsObstacle => true;

        public WallTile(IPosition2D position, IConfigurationService2D configurationService)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.X * configurationService.Dimension;
            Left = position.Y * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Gray;
            SendToBack();
        }
    }
}

