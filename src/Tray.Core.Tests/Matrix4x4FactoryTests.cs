using System.Numerics;

using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class Matrix4x4FactoryTests
    {
        [Fact]
        public void CreateShearing_XY()
        {
            var m = Matrix4x4Factory.CreateShearing(1, 0, 0, 0, 0, 0);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(5, 3, 4, 1)).Should().BeTrue();
        }

        [Fact]
        public void CreateShearing_XZ()
        {
            var m = Matrix4x4Factory.CreateShearing(0, 1, 0, 0, 0, 0);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(6, 3, 4, 1)).Should().BeTrue();
        }

        [Fact]
        public void CreateShearing_YX()
        {
            var m = Matrix4x4Factory.CreateShearing(0, 0, 1, 0, 0, 0);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(2, 5, 4, 1)).Should().BeTrue();
        }

        [Fact]
        public void CreateShearing_YZ()
        {
            var m = Matrix4x4Factory.CreateShearing(0, 0, 0, 1, 0, 0);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(2, 7, 4, 1)).Should().BeTrue();
        }

        [Fact]
        public void CreateShearing_ZX()
        {
            var m = Matrix4x4Factory.CreateShearing(0, 0, 0, 0, 1, 0);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(2, 3, 6, 1)).Should().BeTrue();
        }

        [Fact]
        public void CreateShearing_ZY()
        {
            var m = Matrix4x4Factory.CreateShearing(0, 0, 0, 0, 0, 1);
            var p = new Vector4(2, 3, 4, 1);

            var actual = Vector4.Transform(p, m);

            actual.AboutEquals(new Vector4(2, 3, 7, 1)).Should().BeTrue();
        }
    }
}
