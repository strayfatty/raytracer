using System;

namespace Tray.Core
{
    public static class FloatExtensions
    {
        public static bool AboutEquals(this float a, float b)
        {
            return MathF.Abs(a - b) <= 0.00001f;
        }
    }
}
