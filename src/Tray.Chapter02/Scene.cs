using System.Numerics;

using Tray.Application;
using Tray.Core;

namespace Tray.Chapter02
{
    public sealed class Scene
    {
        private readonly ICanvas canvas;

        private Vector2 origin;

        private Vector2 velocity;

        private Vector2 environment;

        private int stepCount;

        private Vector2 currPos;

        private Vector2 currVel;

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
            this.origin = origin;
            this.velocity = Vector2.Normalize(direction) * velocity;
            this.environment = new Vector2(wind, -gravity);
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

                this.canvas.SetColor(this.currPos, Color.White);
            }

            return true;
        }
    }
}
