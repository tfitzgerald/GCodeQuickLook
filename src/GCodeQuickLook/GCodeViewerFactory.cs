using QuickLook.Plugin;

namespace GCodeQuickLook
{
    public class GCodeViewerFactory : IViewerFactory
    {
        public IViewer CreateViewer()
        {
            return new GCodeViewer();
        }
    }
}
