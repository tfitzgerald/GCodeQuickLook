using System.Collections.Generic;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

namespace GCodeQuickLook
{
    public class GCodeRenderer
    {
        public Element3D BuildToolpathModel(IEnumerable<GCodeSegment> segments)
        {
            var builder = new LineBuilder();

            foreach (var seg in segments)
                builder.AddLine(seg.Start, seg.End);

            var geometry = builder.ToLineGeometry3D();

            return new LineGeometryModel3D
            {
                Geometry = geometry,
                Thickness = 1.0
                // Intentionally NOT setting Color to avoid Color4 vs Media.Color mismatch
            };
        }
    }
}
