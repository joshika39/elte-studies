using Bomber.UI.Shared.Units;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Objects.Player
{
    public partial class Player : UserControl, IPlayerView
    {
        private readonly IConfigurationService2D _configurationService;

        public Player(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension - 4;
            Height = _configurationService.Dimension - 4;
        }

        public void UpdatePosition(IPosition2D position)
        {
            Top = position.Y * _configurationService.Dimension + 2;
            Left = position.X * _configurationService.Dimension + 2;
            BringToFront();
        }
    }
}
