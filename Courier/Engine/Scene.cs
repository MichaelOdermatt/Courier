using Courier.Content;
using Courier.Engine.Collisions;
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
        /// The root Node for the scene.
        /// </summary>
        protected Node root;

        /// <summary>
        /// The player Node for the scene. Does not exist in the game tree (as a descendant of the root Node).
        /// </summary>
        protected Player player;

        /// <summary>
        /// Calls Draw recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance initialized in Game1.</param>
        /// <param name="assetManager">The AssetManager instance initialized in Game1.</param>
        public void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            root.Draw(spriteBatch, assetManager, Vector2.Zero);
            player.Draw(spriteBatch, assetManager, Vector2.Zero);
        }

        /// <summary>
        /// Calls Update recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public void Update(GameTime gameTime)
        {
            // A list of all Nodes in the scene tree that need to have collision checks done on them.
            var collisionNodes = new List<ICollisionNode>();
            // TODO retreive all collision node by iterating through the entire scene tree?

            root.Update(gameTime);
            player.Update(gameTime);
        }
    }
}
