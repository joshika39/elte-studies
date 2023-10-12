using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Objects
{
    public partial class Enemy : UserControl, IUnit2D
    {
        private readonly IPlayer2D _player2D;
        
        public Enemy(IPlayer2D player2D, IPosition2D position)
        {
            _player2D = player2D ?? throw new ArgumentNullException(nameof(player2D));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
        }


        public void SteppedOn(IUnit2D unit2D)
        {
            
        }
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        public void Step(IMapObject2D mapObject)
        {
            throw new NotImplementedException();
        }
    }
}

