using System;
using System.Text;

namespace Tray.Core
{
    public static class CanvasExtensions
    {
        public static string ToPPM(this ICanvas canvas)
        {
            var s = new StringBuilder();
            s.Append("P3\n");
            s.Append($"{canvas.Width} {canvas.Height}\n");
            s.Append("255");

            int remainder = 0;
            for (var y = 0; y < canvas.Height; ++y)
            {
                for (var x = 0; x < canvas.Width; ++x)
                {
                    var color = canvas[x, y];
                    var red = ColorValue(color.R);
                    var green = ColorValue(color.G);
                    var blue = ColorValue(color.B);

                    remainder = Append(red);
                    remainder = Append(green);
                    remainder = Append(blue);
                }

                if (s[s.Length - 1] != '\n')
                {
                    s.Append("\n");
                    remainder = 70;
                }
            }

            if (s[s.Length - 1] != '\n')
                s.Append("\n");

            return s.ToString();

            string ColorValue(float value)
            {
                var intValue = (int)MathF.Round(value * 255, MidpointRounding.AwayFromZero);
                var clamped = MathF.Max(0, MathF.Min(intValue, 255));
                return $"{clamped}";
            }

            int Append(string value)
            {
                var separator = s[s.Length - 1] != '\n' ? " " : string.Empty;
                var newRemainder = remainder - (1 + value.Length);
                if (newRemainder < 1)
                {
                    separator = "\n";
                    newRemainder = 70 - value.Length;
                }

                s.Append(separator);
                s.Append(value);
                return newRemainder;

            }
        }
    }
}
