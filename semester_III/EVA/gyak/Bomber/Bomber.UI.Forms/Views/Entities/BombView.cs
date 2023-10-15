using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public partial class BombView : UserControl, IBombView
    {
        private readonly IConfigurationService2D _configurationService;
        
        public BombView(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension / 2;
            Height = _configurationService.Dimension / 2;
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            BringToFront();
            Top = position.Y * _configurationService.Dimension + _configurationService.Dimension / 4;
            Left = position.X * _configurationService.Dimension + _configurationService.Dimension / 4;
        }
    }
}
