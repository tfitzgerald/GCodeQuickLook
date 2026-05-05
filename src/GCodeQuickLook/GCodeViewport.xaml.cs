using System.Windows.Controls;
using HelixToolkit.Wpf.SharpDX;

namespace GCodeQuickLook
{
    public partial class GCodeViewport : UserControl
    {
        private readonly GroupModel3D _toolpathGroup = new GroupModel3D();

        public GCodeViewport()
        {
            InitializeComponent();
            Viewport.Items.Add(_toolpathGroup);
        }

        public void SetToolpath(Element3D model)
        {
            _toolpathGroup.Children.Clear();
            if (model != null)
                _toolpathGroup.Children.Add(model);
        }
    }
}
