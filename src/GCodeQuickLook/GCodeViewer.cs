using System.IO;
using System.Threading.Tasks;
using System.Windows;
using QuickLook.Plugin;
using QuickLook.Plugin.Common;

namespace GCodeQuickLook
{
    public class GCodeViewer : IViewer
    {
        private GCodeViewport _control;
        private readonly GCodeParser _parser = new GCodeParser();
        private readonly GCodeRenderer _renderer = new GCodeRenderer();

        public bool CanHandle(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext == ".gcode" || ext == ".nc" || ext == ".tap" || ext == ".cnc";
        }

        public async Task<ViewResult> ViewAsync(string path, ContextObject context)
        {
            _control = new GCodeViewport();

            await Task.Run(() =>
            {
                var lines = File.ReadAllLines(path);
                var segments = _parser.ParseLines(lines);
                var model = _renderer.BuildToolpathModel(segments);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _control.SetToolpath(model);
                });
            });

            return new ViewResult
            {
                Title = Path.GetFileName(path),
                View = _control
            };
        }

        public void Cleanup()
        {
            _control = null;
        }
    }
}
