using FluentAssertions;

using Xunit;

namespace Tray.Core.Tests
{
    public sealed class CanvasExtensionTests
    {
        [Fact]
        public void ToPPM_Header()
        {
            var canvas = new Canvas(5, 3);

            var actual = canvas.ToPPM();

            actual.Should().StartWith("P3\n5 3\n255\n");
        }

        [Fact]
        public void ToPPM_PixelData()
        {
            var canvas = new Canvas(5, 3);
            canvas[0, 0] = new Color(1.5f, 0.0f, 0.0f);
            canvas[2, 1] = new Color(0.0f, 0.5f, 0.0f);
            canvas[4, 2] = new Color(-0.5f, 0.0f, 1.0f);

            var actual = canvas.ToPPM();

            var line4 = "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n";
            var line5 = "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n";
            var line6 = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n";
            actual.Should().EndWith(line4 + line5 + line6);
        }

        [Fact]
        public void ToPPM_SplittingLongLines()
        {
            var canvas = new Canvas(10, 2);
            var color = new Color(1.0f, 0.8f, 0.6f);

            for (var x = 0; x < 10; ++x)
                for (var y = 0; y < 2; ++y)
                    canvas[x, y] = color;

            var actual = canvas.ToPPM();

            var line4 = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n";
            var line5 = "153 255 204 153 255 204 153 255 204 153 255 204 153\n";
            var line6 = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n";
            var line7 = "153 255 204 153 255 204 153 255 204 153 255 204 153\n";
            actual.Should().EndWith(line4 + line5 + line6 + line7);
        }

        [Fact]
        public void ToPPM_TerminatedByANewline()
        {
            var canvas = new Canvas(5, 3);

            var actual = canvas.ToPPM();

            actual.Should().EndWith("\n");
        }
    }
}
