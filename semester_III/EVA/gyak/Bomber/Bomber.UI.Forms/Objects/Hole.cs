using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Objects
{
    public partial class Hole : UserControl, IMapObject2D
    {
        private readonly IConfigurationService2D _configurationService;
        public void SteppedOn(IUnit2D unit2D)
        {
            _configurationService.GameIsRunning = false;
            unit2D.Step(this);
        }
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        
        public Hole(IPosition2D position, IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.X * configurationService.Dimension;
            Left = position.Y * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Black;
            SendToBack();
        }
    }
}

