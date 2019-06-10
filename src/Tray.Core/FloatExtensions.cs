using System;

namespace Tray.Core
{
    public static class FloatExtensions
    {
        public static bool AboutEquals(this float a, float b)
        {
            return MathF.Abs(a - b) <= 0.00001f;
        }

        public static byte ClampToByte(this float f)
        {
            var i = (int)Math.Round(f * 255.0f, MidpointRounding.AwayFromZero);
            return (byte)Math.Max(0, Math.Min(i, 255));
        }
    }
}
