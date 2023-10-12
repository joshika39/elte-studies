using Bomber.BL.Map;
using Bomber.BL.Player;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Map;

namespace Bomber.BL.Impl.Player
{
    public class PlayerModel : IBomber
    {
        private readonly IPositionFactory _positionFactory;
        public IPosition2D Position { get; private set; }
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        
        public event EventHandler<IPosition2D> Moved;

        public void Move(MoveDirection moveDirection, IMap2D map)
        {
            switch (moveDirection)
            {
                case MoveDirection.Up:
                    if (Position.Y + 1 < map.SizeY)
                    {
                        Position = _positionFactory.CreatePosition(Position.X, Position.Y + 1);
                        Moved.Invoke(this, Position);
                    }
                    break;
                case MoveDirection.Down:
                    if (Position.Y - 1 >= 0)
                    {
                        Position = _positionFactory.CreatePosition(Position.X, Position.Y - 1);
                        Moved.Invoke(this, Position);
                    }
                    break;
                case MoveDirection.Left:
                    if (Position.X - 1 >= 0)
                    {
                        Position = _positionFactory.CreatePosition(Position.X - 1, Position.Y);
                        Moved.Invoke(this, Position);
                    }
                    break;
                case MoveDirection.Right:
                    if (Position.X + 1 < map.SizeX)
                    {
                        Position = _positionFactory.CreatePosition(Position.X + 1, Position.Y);
                        Moved.Invoke(this, Position);
                    }
                    break;
            }
        }

        public PlayerModel(IPosition2D position, IPositionFactory positionFactory, string name, string email)
        {
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            Position = position;
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
        }
    }
}
