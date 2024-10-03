using Courier.Content;
using Courier.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    public class Scene
    {
        /// <summary>
        /// The root node for the scene.
        /// </summary>
        protected Node Root { get; set; }

        /// <summary>
        /// Calls Draw recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance initialized in Game1.</param>
        /// <param name="assetManager">The AssetManager instance initialized in Game1.</param>
        public void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            Root.Draw(spriteBatch, assetManager, Vector2.Zero);
        }

        /// <summary>
        /// Calls Update recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public void Update(GameTime gameTime)
        {
            Root.Update(gameTime);
        }
    }
}
