using System;
using System.Collections.Generic;
using System.Globalization;
using SharpDX;

namespace GCodeQuickLook
{
    public class GCodeSegment
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public bool IsRapid { get; set; }
    }

    public class GCodeParser
    {
        public IList<GCodeSegment> ParseLines(IEnumerable<string> lines)
        {
            var segments = new List<GCodeSegment>();
            var current = new Vector3(0, 0, 0);
            var last = current;
            bool isRapid = false;

            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;
                var line = raw.Trim();
                if (line.StartsWith(";") || line.StartsWith("(")) continue;

                var tokens = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                string g = null;
                float? x = null, y = null, z = null;

                foreach (var t in tokens)
                {
                    var up = t.ToUpperInvariant();
                    if (up.StartsWith("G")) g = up;
                    else if (up.StartsWith("X"))
                    {
                        float vx;
                        if (float.TryParse(up.Substring(1), NumberStyles.Float, CultureInfo.InvariantCulture, out vx))
                            x = vx;
                    }
                    else if (up.StartsWith("Y"))
                    {
                        float vy;
                        if (float.TryParse(up.Substring(1), NumberStyles.Float, CultureInfo.InvariantCulture, out vy))
                            y = vy;
                    }
                    else if (up.StartsWith("Z"))
                    {
                        float vz;
                        if (float.TryParse(up.Substring(1), NumberStyles.Float, CultureInfo.InvariantCulture, out vz))
                            z = vz;
                    }
                }

                if (g == "G0" || g == "G00") isRapid = true;
                else if (g == "G1" || g == "G01") isRapid = false;
                else continue;

                last = current;
                current = new Vector3(x ?? current.X, y ?? current.Y, z ?? current.Z);

                if (current != last)
                {
                    segments.Add(new GCodeSegment
                    {
                        Start = last,
                        End = current,
                        IsRapid = isRapid
                    });
                }
            }

            return segments;
        }
    }
}
