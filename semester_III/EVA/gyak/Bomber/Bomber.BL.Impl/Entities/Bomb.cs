using Bomber.BL.Entities;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.BL.Impl.Entities
{
    public class Bomb : IBomb
    {
        private readonly IBombView _view;
        private readonly CancellationToken _stoppingToken;
        private PeriodicTimer? _timer;
        private readonly IEnumerable<IMapObject2D> _affectedObjects;
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        public int Radius { get; }

        public Bomb(IBombView view, IPosition2D position, int radius, IConfigurationService2D configurationService, CancellationToken stoppingToken)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _stoppingToken = stoppingToken;
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Radius = radius;
            
            if (!configurationService.GameIsRunning)
            {
                Dispose();
            }
            
            var map = configurationService.GetActiveMap<IBomberMap>();
            _affectedObjects = map!.MapPortion(position, radius);
            _view.Load += OnViewLoaded;
        }
        
        private void OnViewLoaded(object? sender, EventArgs e)
        {
            _view.UpdatePosition(Position);
        }

        public async Task Detonate()
        {
            var countDownPeriod = 2d;
            while (!_stoppingToken.IsCancellationRequested)
            {
                
                countDownPeriod -= 0.3;
                
                
                if(countDownPeriod <= 0)
                {
                    Explode();
                    break;
                }
                
                var time = TimeSpan.FromSeconds(countDownPeriod);

                _timer = new PeriodicTimer(time);

                if (await _timer.WaitForNextTickAsync(_stoppingToken))
                {
                    foreach (var affectedObject in _affectedObjects)
                    {
                        if(affectedObject is IBomberMapTileView bombMapObject)
                        {
                            bombMapObject.IndicateBomb(countDownPeriod / 10);
                        }
                    }
                }
                
            }
            
            Dispose();
        }

        private void Explode()
        {
            Console.WriteLine($"Bomb exploded at {Position.X}, {Position.Y}");
        }
        
        public void SteppedOn(IUnit2D unit2D)
        {
            
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
            _view.Dispose();
        }
    }
}
