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

        private const float parallaxSpeed = 0.5f;

        /// <summary>
        /// The Y position that the camera must be at to reach the max end of the parallax effect.
        /// </summary>
        private const float maxYParallaxOffset = 600f;
        /// <summary>
        /// The Y position that the camera must be at to reach the min end of the parallax effect.
        /// </summary>
        private const float minYParallaxOffset = 200f;

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
            // Get the camera's Y pos as a value relative to the min and max parallax offsets.
            var yPosAsPercentage = (camera.Position.Y - minYParallaxOffset) / (maxYParallaxOffset - minYParallaxOffset);
            // Scale the y pos so that it matches the desired parallax speed
            var scaledYPos = yPosAsPercentage * parallaxSpeed;
            // Apply a smoothstep function to the Y pos so that paralax movement looks more gradual.
            var yPosSmoothStepped = 1 - MathHelper.SmoothStep(0, 1, Math.Clamp(scaledYPos, 0f, 1f));
            // Convert the smoothstepped Y pos to a new Y coordinate.
            return yPosSmoothStepped * (maxYSpriteOffset - minYSpriteOffset) + minYSpriteOffset;
        }
    }
}