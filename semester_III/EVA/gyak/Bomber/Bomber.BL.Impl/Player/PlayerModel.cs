using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Player
{
    public class PlayerModel : IPlayer2D
    {
        private readonly IPositionFactory _positionFactory;
        private readonly IConfigurationService2D _configurationService2D;
        private bool _isAlive = true;
        public IPosition2D Position { get; private set; }
        public bool IsObstacle => false;
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }

        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotImplementedException();
        }

        public void Step(IMapObject2D mapObject)
        {
            if (!_configurationService2D.GameIsRunning)
            {
                if (!_isAlive)
                {
                    return;
                }

                _isAlive = false;
            }
            Position = _positionFactory.CreatePosition(mapObject.Position.Y, mapObject.Position.X);
        }

        public PlayerModel(IPosition2D position, IPositionFactory positionFactory, IConfigurationService2D configurationService2D, string name, string email)
        {
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            Position = position;
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
        }
    }
}
