using System.Numerics;

namespace Tray.Core
{
    public static class Vector4Extensions
    {
        public static bool AboutEquals(this Vector4 a, Vector4 b)
        {
            return a.X.AboutEquals(b.X)
                && a.Y.AboutEquals(b.Y)
                && a.Z.AboutEquals(b.Z)
                && a.W.AboutEquals(b.W);
        }
    }
}
