using UiFramework.Shared;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.MapGenerator
{
    public partial class MapGeneratorWindow : Form, IMapGeneratorWindow
    {
        public MapGeneratorWindow()
        {
            InitializeComponent();
        }
        
        public DialogResult ShowOnTop()
        {
            throw new NotImplementedException();
        }
    }
}

