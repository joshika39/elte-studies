using Bomber.UI.Shared.Units;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Player
{
    public class PlayerModel : IPlayer2D
    {
        private readonly IPlayerView _view;
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
            
            Position = mapObject.Position;
            _view.UpdatePosition(Position);
        }

        public PlayerModel(IPlayerView view, IPosition2D position, IConfigurationService2D configurationService2D, string name, string email)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            Position = position;
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
            _view.Load += OnViewLoad;
        }
        
        private void OnViewLoad(object? sender, EventArgs e)
        {
            _view.UpdatePosition(Position);
        }
    }
}
