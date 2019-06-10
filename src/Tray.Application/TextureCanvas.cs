using System;

using Tray.Core;

using Veldrid;

namespace Tray.Application
{
    public sealed class TextureCanvas : ICanvas
    {
        private readonly GraphicsDevice device;

        private readonly ResourceFactory factory;

        private byte[] data;

        private Texture staging;

        private Texture texture;

        public TextureCanvas(
            GraphicsDevice device,
            ResourceFactory factory,
            int width,
            int height)
        {
            this.device = device;
            this.factory = factory;

            this.Resize(width, height);
        }

        public Color this[int x, int y]
        {
            get
            {
                var index = (y * this.Width + x) * 4;
                return Color.CopyFrom(this.data, index);
            }

            set
            {
                if (x < 0 || x >= this.Width)
                    return;

                if (y < 0 || y >= this.Height)
                    return;

                var index = (y * this.Width + x) * 4;
                value.CopyTo(this.data, index);
                this.data[index + 3] = (byte)255;
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public TextureView View { get; private set; }

        public void Clear(Color color)
        {
            var r = color.R.ClampToByte();
            var g = color.G.ClampToByte();
            var b = color.B.ClampToByte();
            for (var i = 0; i < this.data.Length;)
            {
                this.data[i++] = r;
                this.data[i++] = g;
                this.data[i++] = b;
                this.data[i++] = 255;
            }
        }

        public void Resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.data = new byte[width * height * 4];
            this.Clear(Color.Black);
            this.CreateResources();
            this.UpdateTexture();
        }

        public unsafe void UpdateTexture()
        {
            fixed (byte* dataPtr = &this.data[0])
            {
                this.device.UpdateTexture(
                    this.staging,
                    (IntPtr)dataPtr,
                    (uint)this.data.Length,
                    0, 0, 0, (uint)this.Width, (uint)this.Height, 1, 0, 0);
            }

            var commandList = this.factory.CreateCommandList();
            commandList.Begin();
            commandList.CopyTexture(this.staging, this.texture);
            commandList.End();
            this.device.SubmitCommands(commandList);
        }

        private void CreateResources()
        {
            this.staging = this.factory.CreateTexture(
                new TextureDescription
                {
                    Width = (uint)this.Width,
                    Height = (uint)this.Height,
                    Depth = 1,
                    MipLevels = 1,
                    ArrayLayers = 1,
                    Format = PixelFormat.B8_G8_R8_A8_UNorm,
                    Usage = TextureUsage.Staging,
                    Type = TextureType.Texture2D
                });

            this.texture = this.factory.CreateTexture(
                new TextureDescription
                {
                    Width = (uint)this.Width,
                    Height = (uint)this.Height,
                    Depth = 1,
                    MipLevels = 1,
                    ArrayLayers = 1,
                    Format = PixelFormat.B8_G8_R8_A8_UNorm,
                    Usage = TextureUsage.Sampled,
                    Type = TextureType.Texture2D
                });

            this.View = this.factory.CreateTextureView(this.texture);
        }
    }
}
