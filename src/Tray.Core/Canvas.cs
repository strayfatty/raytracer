namespace Tray.Core
{
    public sealed class Canvas : ICanvas
    {
        private Color[,] data;

        public Canvas(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.data = new Color[width, height];
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Color this[int x, int y]
        {
            get => this.data[x, y];

            set
            {
                if (x < 0 || x >= this.Width)
                    return;

                if (y < 0 || y >= this.Height)
                    return;

                this.data[x, y] = value;
            }
        }

        public void Clear(Color color)
        {
            for (var y = 0; y < this.Height; ++y)
            {
                for (var x = 0; x < this.Width; ++x)
                {
                    this.data[x, y] = color;
                }
            }
        }

        public void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.data = new Color[width, height];
        }
    }
}
