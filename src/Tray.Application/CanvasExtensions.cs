using System;
using System.Numerics;

using Tray.Core;

namespace Tray.Application
{
    public static class CanvasExtensions
    {
        public static void SetColor(
            this ICanvas canvas,
            Vector2 point,
            Color color)
        {
            SetColor(canvas, point.X, point.Y, color);
        }

        public static void SetColor(
        this ICanvas canvas,
        float x,
        float y,
        Color color)
        {
            var ix = (int)MathF.Round(x, MidpointRounding.AwayFromZero);
            var iy = (int)MathF.Round(y, MidpointRounding.AwayFromZero);

            canvas[ix, canvas.Height - iy] = color;
        }
    }
}
