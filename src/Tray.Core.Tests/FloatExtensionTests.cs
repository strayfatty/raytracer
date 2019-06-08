using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class FloatExtensionTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void AboutEquals(float a, float b, bool expected)
        {
            a.AboutEquals(b).Should().Be(expected);
        }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { 1.0f, 1.0f, true },
            new object[] { 1.0f, 2.0f, false },
            new object[] { 1.0f, 1.1f, false },
            new object[] { 1.0f, 1.01f, false },
            new object[] { 1.0f, 1.001f, false },
            new object[] { 1.0f, 1.0001f, false },
            new object[] { 1.0f, 1.00001f, false },
            new object[] { 1.0f, 1.000001f, true },
            new object[] { 1.0f, 1.0000001f, true },
            new object[] { 1.0f, 1.00000001f, true },
            new object[] { 0.0f, 0.0000001f, true },
        };
    }
}
