namespace QuickLook.Plugin
{
    public interface IViewerFactory
    {
        IViewer CreateViewer();
    }

    public interface IViewer
    {
        bool CanHandle(string path);
        System.Threading.Tasks.Task<QuickLook.Plugin.Common.ViewResult> ViewAsync(string path, QuickLook.Plugin.Common.ContextObject context);
        void Cleanup();
    }
}

namespace QuickLook.Plugin.Common
{
    public class ContextObject
    {
        public System.Windows.Window ParentWindow { get; set; }
    }

    public class ViewResult
    {
        public string Title { get; set; }
        public object View { get; set; }
    }
}
