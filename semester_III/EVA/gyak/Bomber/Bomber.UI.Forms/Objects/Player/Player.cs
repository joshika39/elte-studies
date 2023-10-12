using Bomber.BL.Map;
using Bomber.BL.Player;
using Bomber.UI.Forms.Objects.Player._Interfaces;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Map;
using UiFramework.Forms;

namespace Bomber.UI.Forms.Objects.Player
{
    public partial class Player : UserControl, IPlayerView
    {
        private readonly IMap2D? _map;
        private readonly IConfigurationService _configurationService;
        
        public IPlayerViewPresenter Presenter { get; }
        
        public Player(IPlayerViewPresenter presenter, IConfigurationService configurationService)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Top = _configurationService.Dimension;
            Left = _configurationService.Dimension;
            Width = _configurationService.Dimension - 2;
            Height = _configurationService.Dimension - 2;
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (_map == null)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.D:
                    Presenter.Move(MoveDirection.Right, _map);
                    break;
                case Keys.A:
                    Presenter.Move(MoveDirection.Left, _map);
                    break;
                case Keys.W:
                    Presenter.Move(MoveDirection.Up, _map);
                    break;
                case Keys.S:
                    Presenter.Move(MoveDirection.Down, _map);
                    break;
            }
        }
        public IPosition2D Position { get; }
        public Guid Id { get; }
        public string Email { get; }
        public event EventHandler<IPosition2D>? Moved;
        public void Move(MoveDirection moveDirection, IMap2D map)
        {
            throw new NotImplementedException();
        }
    }
}

