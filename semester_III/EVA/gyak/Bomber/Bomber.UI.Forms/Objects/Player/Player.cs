using Bomber.BL.Map;
using GameFramework.Core;

namespace Bomber.UI.Forms.Objects.Player
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

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.D)
            {

            }

            if (e.KeyCode == Keys.A)
            {

            }

            if (e.KeyCode == Keys.W)
            {

            }

            if (e.KeyCode == Keys.S)
            {

            }
        }
    }
}

