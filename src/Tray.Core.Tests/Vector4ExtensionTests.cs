using System.Collections.Generic;
using System.Numerics;

using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class Vector4ExtensionTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void AboutEquals(
            float[] a,
            float[] b,
            bool expected)
        {
            var lhs = new Vector4(a[0], a[1], a[2], a[3]);
            var rhs = new Vector4(b[0], b[1], b[2], b[3]);

            lhs.AboutEquals(rhs).Should().Be(expected);
        }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[]
            {
                new float[] { 1.0f, 1.0f, 1.0f, 1.0f },
                new float[] { 1.0f, 1.0f, 1.0f, 1.0f },
                true
            },
            new object[]
            {
                new float[] { 1.0f, 1.0f, 1.0f, 1.0f },
                new float[] { 2.0f, 2.0f, 2.0f, 2.0f },
                false
            },
            new object[]
            {
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.0f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 0.01f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                false
            },
            new object[]
            {
                new float[] { 0.001f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                false
            },
            new object[]
            {
                new float[] { 0.0001f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                false
            },
            new object[]
            {
                new float[] { 0.00001f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                true
            },
            new object[]
            {
                new float[] { 0.000001f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                true
            },
            new object[]
            {
                new float[] { 0.0000001f, 0.0f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f },
                true
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.01f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.001f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.0001f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.00001f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.000001f, 0.0f, 1.0f },
                false
            },
            new object[]
            {
                new float[] { 1.0f, 0.0f, 0.0f, 1.0f },
                new float[] { 0.0f, 1.0000001f, 0.0f, 1.0f },
                false
            }
        };
    }
}
