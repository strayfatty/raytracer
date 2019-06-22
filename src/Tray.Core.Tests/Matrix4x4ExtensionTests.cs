using System.Collections.Generic;
using System.Numerics;

using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class Matrix4x4ExtensionTests
    {
        [Fact]
        public void AboutEquals_IdentityIdentity_True()
        {
            var a = Matrix4x4.Identity;
            var b = Matrix4x4.Identity;

            var actual = a.AboutEquals(b);

            actual.Should().BeTrue();
        }

        [Fact]
        public void AboutEquals_IdentityTranslation_False()
        {
            var a = Matrix4x4.Identity;
            var b = Matrix4x4.CreateTranslation(0.0f, 0.0f, 1.0f);

            var actual = a.AboutEquals(b);

            actual.Should().BeFalse();
        }

        [Fact]
        public void AboutEquals_Inverse_True()
        {
            var a = new Matrix4x4(
                 8.0f, -5.0f,  9.0f,  2.0f,
                 7.0f,  5.0f,  6.0f,  1.0f,
                -6.0f,  0.0f,  9.0f,  6.0f,
                -3.0f,  0.0f, -9.0f, -4.0f);
            var b = new Matrix4x4(
                -0.15385f, -0.15385f, -0.28205f, -0.53846f,
                -0.07692f,  0.12308f,  0.02564f,  0.03077f,
                 0.35897f,  0.35897f,  0.43590f,  0.92308f,
                -0.69231f, -0.69231f, -0.76923f, -1.92308f);

            Matrix4x4 inverted;
            Matrix4x4.Invert(a, out inverted);
            var actual = inverted.AboutEquals(b);

            actual.Should().BeTrue();
        }
    }
}
