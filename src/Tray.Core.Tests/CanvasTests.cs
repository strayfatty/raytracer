using FluentAssertions;
using Xunit;

namespace Tray.Core.Tests
{
    public sealed class CanvasTests
    {
        [Fact]
        public void Constructor()
        {
            var canvas = new Canvas(10, 20);

            canvas.Width.Should().Be(10);
            canvas.Height.Should().Be(20);

            for (var x = 0; x < 10; ++x)
            {
                for (var y = 0; y < 20; ++y)
                {
                    canvas[x, y].Should().Be(Color.Black);
                }
            }
        }

        [Fact]
        public void Indexer()
        {
            var canvas = new Canvas(5, 5);

            canvas[2, 3] = new Color(1.0f, 2.0f, 3.0f);

            canvas[2, 3].Should().Be(new Color(1.0f, 2.0f, 3.0f));
        }

        [Fact]
        public void Clear()
        {
            var canvas = new Canvas(5, 5);

            canvas.Clear(Color.White);

            for (var x = 0; x < 5; ++x)
            {
                for (var y = 0; y < 5; ++y)
                {
                    canvas[x, y].Should().Be(Color.White);
                }
            }
        }

        [Fact]
        public void Resize()
        {
            var canvas = new Canvas(5, 5);
            canvas.Clear(Color.White);

            canvas.Resize(10, 3);

            canvas.Width.Should().Be(10);
            canvas.Height.Should().Be(3);

            for (var x = 0; x < 10; ++x)
            {
                for (var y = 0; y < 3; ++y)
                {
                    canvas[x, y].Should().Be(Color.Black);
                }
            }
        }
    }
}
