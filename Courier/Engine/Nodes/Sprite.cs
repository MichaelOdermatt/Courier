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
        /// If true, the sprite will not be drawn if it's position is outside the view of the camera.
        /// </summary>
        public bool CullIfNotInView { get; set; } = true;

        public Sprite(Node parent, string textureKey, float layerDepth = 0.0f) : base(parent)
        {
            this.textureKey = textureKey;
            this.layerDepth = layerDepth;
        }

        /// <summary>
        /// Draws the Sprite and calls the Draw function on any child Nodes.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch, AssetManager assetManager, Camera2D camera)
        {
            // Draw all child Nodes
            base.Draw(spriteBatch, assetManager, camera);

            var spritePos = GlobalPosition + Offset;

            // If CullIfNotInView is enabled and the sprite is not in the camera's view, don't draw it. Or if the Sprite's Visible property is false, don't draw it.
            if ((CullIfNotInView && !camera.IsPointInCameraView(spritePos)) || !Visible)
            {
                return;
            }

            var texture = assetManager.Textures[textureKey];
            var origin = Origin ?? new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture, spritePos, null, Color.White, Rotation, origin, Scale, SpriteEffects.None, layerDepth);
        }
    }
}
