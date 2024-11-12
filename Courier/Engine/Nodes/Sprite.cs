using Courier.Engine.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Nodes
{
    /// <summary>
    /// Node used to store a game objects sprite and any sprite related information.
    /// </summary>
    public class Sprite : Node
    {
        /// <summary>
        /// The key which can be used to retrieve the texture from AssetManager during the call to Draw.
        /// </summary>
        private readonly string textureKey;

        /// <summary>
        /// Essentially the Z index of the sprite. Specifies at what layer the sprite should be rendered.
        /// </summary>
        private readonly float layerDepth = 0.0f;

        public Vector2 Offset { get; set; } = Vector2.Zero;

        public float Rotation { get; set; } = 0.0f;

        public Vector2 Scale { get; set; } = Vector2.One;

        /// <summary>
        /// Nullable Vector2 for the Sprite's origin, if left as null the origin will be calculated during the draw call as the center of the sprite.
        /// </summary>
        public Vector2? Origin { get; set; } = null;

        /// <summary>
        /// Boolean representing if the Sprite will be rendered or not.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Boolean representing if the Sprite is intended to be a world space sprite rather than a screen space sprite.
        /// </summary>
        public bool IsWorldSpaceSprite { get; set; } = true;

        public Sprite(Node parent, string textureKey, float layerDepth = 0.0f) : base(parent)
        {
            this.textureKey = textureKey;
            this.layerDepth = layerDepth;
        }

        /// <summary>
        /// Draws the Sprite and calls the Draw function on any child Nodes.
        /// </summary>
        public override void Draw(SpriteRenderer spriteRenderer)
        {
            // Draw all child Nodes
            base.Draw(spriteRenderer);

            if (!Visible)
            {
                return;
            }

            var spritePos = GlobalPosition + Offset;

            // Add the sprite to the renderer so it can be drawn in the next draw call.
            spriteRenderer.AddSprite(new SpriteRenderData
            {
                TextureKey = textureKey,
                Position = spritePos,
                Rotation = Rotation,
                Origin = Origin,
                Scale = Scale,
                LayerDepth = layerDepth,
                IsWorldSpaceSprite = IsWorldSpaceSprite
            });
        }
    }
}
