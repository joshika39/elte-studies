using System.Reflection.Emit;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Shared.Views
{
    public class AMainWindowModel : IMainWindowModel
    {
        private readonly IServiceProvider _provider;
        private readonly IPositionFactory _factory;

        protected IConfigurationService2D ConfigurationService { get; }

        protected AMainWindowModel(IServiceProvider provider, IConfigurationService2D configurationService)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            ConfigurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _factory = _provider.GetRequiredService<IPositionFactory>();
        }

        public IBomberMap OpenMap(string mapFileName)
        {
            var mapLayout = new MapLayout(mapFileName, _provider);
            var map = new Map(
                mapLayout.ColumnCount,
                mapLayout.RowCount,
                new List<IUnit2D>(),
                mapLayout.MapObjects,
                _factory);

            ConfigurationService.SetActiveMap(map);
            ConfigurationService.GameIsRunning = true;
            return map;
        }
    }
}
