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
        private readonly string textureKey;

        /// <summary>
        /// Essentially the Z index of the sprite. Specifies at what layer the sprite should be rendered.
        /// </summary>
        private readonly float layerDepth = 0.0f;

        public Sprite(Node parent, string textureKey, float layerDepth = 0.0f): base(parent)
        {
            this.textureKey = textureKey;
            this.layerDepth = layerDepth;
        }

        /// <summary>
        /// Draws the Sprite and calls the Draw function on any child Nodes.
        /// </summary>
        // TODO get rid of the parent position argument and istead save the parent position as a property or variable of this class. Maybe pass and instance of the parent as an argument to the constructor? 
        public override void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Draw all child Nodes
            base.Draw(spriteBatch, assetManager);

            var texture = assetManager.Textures[textureKey];
            var origin = new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture, GlobalPosition, null, Color.White, 0.0f, origin, Vector2.One, SpriteEffects.None, layerDepth);
        }
    }
}
