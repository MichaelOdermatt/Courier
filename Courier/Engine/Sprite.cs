using Courier.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    /// <summary>
    /// Node used to store a game objects sprite and any sprite related information.
    /// </summary>
    public class Sprite : Node
    {
        /// <summary>
        /// The key which can be used to retrieve the texture from AssetManager during the call to Draw.
        /// </summary>
        private string textureKey;

        /// <summary>
        /// Essentially the Z index of the sprite. Specifies at what layer the sprite should be rendered.
        /// </summary>
        public float LayerDepth { get; set; } = 0.0f;

        public Sprite(string textureKey, float layerDepth = 0.0f)
        {
            this.textureKey = textureKey;
            LayerDepth = layerDepth;
        }

        /// <summary>
        /// Draws the Sprite and calls the Draw function on any child Nodes.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch, AssetManager assetManager, Vector2 parentPosition)
        {
            // Draw all child Nodes
            base.Draw(spriteBatch, assetManager, parentPosition + Position);

            var texture = assetManager.Textures[textureKey];
            var origin = new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture, parentPosition + Position, null, Color.White, 0.0f, origin, Vector2.One, SpriteEffects.None, LayerDepth);
        }
    }
}
