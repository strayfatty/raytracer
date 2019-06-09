namespace Tray.Core
{
    public interface ICanvas
    {
        int Width { get; }

        int Height { get; }

        Color this[int x, int y] { get; set; }

        void Clear(Color color);

        void Resize(int width, int height);
    }
}
