using System;
using System.Numerics;
using Tray.Core;

namespace Tray.Chapter02
{
    public sealed class Scene
    {
        private readonly ICanvas canvas;

        private Vector4 origin;

        private Vector4 velocity;

        private Vector4 environment;

        private int stepCount;

        private Vector4 currPos;

        private Vector4 currVel;

        private int currStep;

        public Scene(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public void Init(
            Vector2 origin,
            Vector2 direction,
            float velocity,
            float gravity,
            float wind,
            int stepCount)
        {
            this.origin = new Vector4(origin, 0, 1);
            this.velocity = Vector4.Normalize(new Vector4(direction, 0, 0)) * velocity;
            this.environment = new Vector4(wind, -gravity, 0.0f, 0.0f);
            this.stepCount = stepCount;

            this.Reset();
        }

        public void Reset()
        {
            this.currPos = this.origin;
            this.currVel = this.velocity;
            this.currStep = 0;
        }

        public bool Update(float deltaSeconds)
        {
            if (this.currStep >= this.stepCount)
                return false;

            var steps = (int)(this.stepCount * deltaSeconds);
            for (var i = 0; i < steps && this.currStep < this.stepCount; ++i)
            {
                this.currStep++;
                this.currPos += this.currVel;
                this.currVel += this.environment;

                var x = (int)Math.Round(this.currPos.X, MidpointRounding.AwayFromZero);
                var y = (int)Math.Round(this.currPos.Y, MidpointRounding.AwayFromZero);

                this.canvas[x, this.canvas.Height - y] = Color.White;
            }

            return true;
        }
    }
}
