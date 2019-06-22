using System;
using System.Numerics;
using Tray.Application;
using Tray.Core;

namespace Tray.Chapter03
{
    public sealed class Scene
    {
        private readonly ICanvas canvas;

        private Vector2 center;

        private float radius;

        private float elapsed = 0.0f;

        public Scene(ICanvas canvas)
        {
            this.canvas = canvas;

            this.Reset();
        }

        public float Frequency { get; set; } = 0.01f;

        public Color ColorClock { get; set; } = Color.White;

        public Color ColorHour { get; set; } = Color.White;

        public Color ColorMinute { get; set; } = Color.White;

        public Color ColorSecond { get; set; } = Color.White;

        public Color ColorMillisecond { get; set; } = Color.White;

        public void Reset()
        {
            var width = this.canvas.Width;
            var height = this.canvas.Height;

            this.center = new Vector2(0.5f * width, 0.5f * height);
            this.radius = 0.5f * Math.Min(width, height) - 50;
        }

        public bool Update(float deltaSeconds)
        {
            this.elapsed += deltaSeconds;
            if (elapsed < this.Frequency)
                return false;

            this.elapsed -= this.Frequency;

            this.canvas.Clear(Color.Black);

            this.DrawClock();

            var time = DateTime.Now;
            var includeMillisecond = this.Frequency <= 0.02;

            this.DrawMillisecondHand(time, includeMillisecond);
            this.DrawSecondHand(time, includeMillisecond);
            this.DrawMinuteHand(time, includeMillisecond);
            this.DrawHourHand(time, includeMillisecond);

            return true;
        }

        private void DrawClock()
        {
            this.DrawBlock(this.center, 2, this.ColorClock);

            var start = new Vector2(0, this.radius);
            for (var i = 0; i < 360; i += 30)
            {
                var radians = i * MathF.PI / 180.0f;
                var position = Vector2.Transform(start, Matrix4x4.CreateRotationZ(-radians));
                this.DrawBlock(this.center + position, 2, this.ColorClock);
            }
        }

        private void DrawMillisecondHand(DateTime time, bool includeMillisecond)
        {
            if (!includeMillisecond)
                return;

            var millisecond = time.Millisecond;
            var degrees = millisecond * 360.0f / 1000.0f;

            this.DrawHand(degrees, 1, (int)this.radius - 10, this.ColorMillisecond);
        }

        private void DrawSecondHand(DateTime time, bool includeMillisecond)
        {
            var millisecond = includeMillisecond ? time.Millisecond : 0;
            var second = time.Second * 1000 + millisecond;
            var degrees = second * 360.0f / 60000.0f;

            this.DrawHand(degrees, 2, (int)this.radius - 20, this.ColorSecond);
        }

        private void DrawMinuteHand(DateTime time, bool includeMillisecond)
        {
            var millisecond = includeMillisecond ? time.Millisecond : 0;
            var second = time.Second * 1000 + millisecond;
            var minute = time.Minute * 60000 + second;
            var degrees = minute * 360.0f / 3600000.0f;

            this.DrawHand(degrees, 3, (int)this.radius - 30, this.ColorMinute);
        }

        private void DrawHourHand(DateTime time, bool includeMillisecond)
        {
            var millisecond = includeMillisecond ? time.Millisecond : 0;
            var second = time.Second * 1000 + millisecond;
            var minute = time.Minute * 60000 + second;
            var hour = time.Hour * 3600000 + minute;
            var degrees = hour * 360.0f / 43200000.0f;

            this.DrawHand(degrees, 4, (int)this.radius - 40, this.ColorHour);
        }

        private void DrawHand(float degree, int width, int length, Color color)
        {
            var radians = degree * MathF.PI / 180.0f;
            var rotation = Matrix4x4.CreateRotationZ(-radians);

            var start = new Vector2(0, length);
            var position = Vector2.Transform(start, rotation);

            this.DrawBlock(this.center + position, width, color);
        }

        private void DrawBlock(Vector2 position, int borderWidth, Color color)
        {
            var x = position.X - borderWidth;
            var y = position.Y - borderWidth;

            var length = borderWidth * 2 + 1;
            for (var dx = 0; dx < length; ++dx)
            {
                for (var dy = 0; dy < length; ++dy)
                {
                    this.canvas.SetColor(x + dx, y + dy, color);
                }
            }
        }
    }
}
