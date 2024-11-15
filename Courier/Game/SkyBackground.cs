using Courier.Engine;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;

namespace Courier.Game
{
    public class SkyBackground : Node
    {
        private readonly Sprite sprite;
        private readonly Camera2D camera;

        private const float maxYSpriteOffset = 0f;
        private const float minYSpriteOffset = -160f;

        private const float paralaxSpeed = 0.2f;
        private const float maxYParalaxOffset = 100f;
        private const float minYParalaxOffset = -100f;

        public SkyBackground(Node parent, Camera2D camera) : base(parent)
        {
            this.camera = camera;

            sprite = new Sprite(this, "Sky");
            sprite.IsWorldSpaceSprite = false;
            sprite.Origin = Vector2.Zero;
            Children.Add(sprite);
        }

        public override void Update(GameTime gameTime) 
        {
            var newYOffset = GetSpriteOffsetY();
            sprite.Offset = new Vector2(0, newYOffset); 
        }

        private float GetSpriteOffsetY()
        {
            var clampedScaledYPos = Math.Clamp(camera.Position.Y * paralaxSpeed, minYParalaxOffset, maxYParalaxOffset);
            // Get the camera's Y pos as a value between 0 and 1.
            var yPosAsFloat = (clampedScaledYPos + maxYParalaxOffset) / (Math.Abs(maxYParalaxOffset) + Math.Abs(minYParalaxOffset));
            // Apply a smoothstep function to the Y pos so that paralax movement looks more gradual.
            var yPosSmoothStepped = 1 - MathHelper.SmoothStep(0, 1, yPosAsFloat);
            // Convert the smoothstepped Y pos to a new Y coordinate.
            return yPosSmoothStepped * (Math.Abs(minYSpriteOffset) + Math.Abs(maxYSpriteOffset)) + minYSpriteOffset;
        }
    }
}