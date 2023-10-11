using Bomber.BL.Map;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.Objects
{
    public partial class Player : UserControl, IBomber 
    {
        private EventHandler<IPosition2D> _moved;
        public IPosition2D Position { get; }
        public event EventHandler<IPosition2D> Moved;
        
        public Player()
        {
            InitializeComponent();
        }

        private void PlayerMove()
        {
            Moved.Invoke(this, Position);
        }

        public Guid Id { get; }
        public string Email { get; }    
    }
}

