using System.Numerics;

namespace Tray.Core
{
    public static class Matrix4x4Factory
    {
        public static Matrix4x4 CreateShearing(
            float xy,
            float xz,
            float yx,
            float yz,
            float zx,
            float zy)
        {
            return new Matrix4x4(
                1, yx, zx, 0,
                xy, 1, zy, 0,
                xz, yz, 1, 0,
                0, 0, 0, 1);
        }
    }
}
