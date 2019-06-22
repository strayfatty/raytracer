using System.Numerics;

namespace Tray.Core
{
    public static class Matrix4x4Extensions
    {
        public static bool AboutEquals(this Matrix4x4 a, Matrix4x4 b)
        {
            return a.M11.AboutEquals(b.M11)
                && a.M12.AboutEquals(b.M12)
                && a.M13.AboutEquals(b.M13)
                && a.M14.AboutEquals(b.M14)
                && a.M21.AboutEquals(b.M21)
                && a.M22.AboutEquals(b.M22)
                && a.M23.AboutEquals(b.M23)
                && a.M24.AboutEquals(b.M24)
                && a.M31.AboutEquals(b.M31)
                && a.M32.AboutEquals(b.M32)
                && a.M33.AboutEquals(b.M33)
                && a.M34.AboutEquals(b.M34)
                && a.M41.AboutEquals(b.M41)
                && a.M42.AboutEquals(b.M42)
                && a.M43.AboutEquals(b.M43)
                && a.M44.AboutEquals(b.M44);
        }
    }
}
