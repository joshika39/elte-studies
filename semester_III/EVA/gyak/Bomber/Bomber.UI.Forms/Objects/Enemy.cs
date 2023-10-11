using Bomber.BL.Map;
using GameFramework.Core;
using GameFramework.Map.MapObject;

namespace Bomber.Objects
{
    public partial class Enemy : UserControl, IMapObject2D
    {
        private readonly IBomber _player2D;
        
        public Enemy(IBomber player2D)
        {
            _player2D = player2D ?? throw new ArgumentNullException(nameof(player2D));
            InitializeComponent();

            _player2D.Moved += OnPlayerMoved;
        }
        private void OnPlayerMoved(object? sender, IPosition2D e)
        {
            throw new NotImplementedException();
        }
        
        public IPosition2D Position { get; }
    }
}

