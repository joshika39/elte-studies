using Bomber.UI.Forms.Objects.Player._Interfaces;
using GameFramework.Configuration;
using GameFramework.Core.Motion;
using GameFramework.Entities;

namespace Bomber.UI.Forms.Objects.Player
{
    public partial class Player : UserControl, IPlayerView
    {
        private readonly IPlayer2D _model;
        private readonly IConfigurationService2D _configurationService;

        public Player(IPlayer2D model, IConfigurationService2D configurationService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Top = model.Position.Y * _configurationService.Dimension + 2;
            Left = model.Position.X * _configurationService.Dimension + 2;
            Width = _configurationService.Dimension - 4;
            Height = _configurationService.Dimension - 4;
            BringToFront();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (!_configurationService.GameIsRunning)
            {
                return;
            }
            
            var map = _configurationService.ActiveMap;
            
            if (e.KeyCode == Keys.D)
            {
                map?.MoveUnit(_model, Move2D.Right);
                UpdatePosition();
            }

            if (e.KeyCode == Keys.A)
            {
                map?.MoveUnit(_model, Move2D.Left);
                UpdatePosition();
            }

            if (e.KeyCode == Keys.W)
            {
                map?.MoveUnit(_model, Move2D.Forward);
                UpdatePosition();
            }

            if (e.KeyCode == Keys.S)
            {
                map?.MoveUnit(_model, Move2D.Backward);
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            Top = _model.Position.Y * _configurationService.Dimension + 2;
            Left = _model.Position.X * _configurationService.Dimension + 2;
        }
    }
}
