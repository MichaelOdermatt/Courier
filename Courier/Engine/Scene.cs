using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
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
    public abstract class Scene
    {
        /// <summary>
        /// The root Node for the scene's screen space nodes.
        /// </summary>
        protected Node screenSpaceRoot;

        /// <summary>
        /// The root Node for the scene's world space nodes.
        /// </summary>
        protected Node worldSpaceRoot;

        /// <summary>
        /// The player Node for the scene. Does not exist in the game tree (as a descendant of the root Node).
        /// </summary>
        protected PlayerController player;

        /// <summary>
        /// The camera used in the Scene.
        /// </summary>
        protected Camera2D camera;

        public Scene(Camera2D camera)
        {
            this.camera = camera;
        }

        /// <summary>
        /// This method is intended to be used for creating instances of and intializing the Scenes's Nodes.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Calls Draw recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="worldSpaceSpriteBatch">The SpriteBatch instance initialized in Game1, used for world space objects like the player, enemies... etc.</param>
        /// <param name="screenSpaceSpriteBatch">The SpriteBatch instance initialized in Game1, used for screen space objects like the UI.</param>
        /// <param name="assetManager">The AssetManager instance initialized in Game1.</param>
        public void Draw(SpriteBatch worldSpaceSpriteBatch, SpriteBatch screenSpaceSpriteBatch, AssetManager assetManager)
        {
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            screenSpaceRoot.Draw(screenSpaceSpriteBatch, assetManager, camera);
            worldSpaceRoot.Draw(worldSpaceSpriteBatch, assetManager, camera);
            player.Draw(worldSpaceSpriteBatch, assetManager, camera);
        }

        /// <summary>
        /// Calls Update recursively on the root node until all it's children are updated.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public void Update(GameTime gameTime)
        {
            screenSpaceRoot.Update(gameTime);
            worldSpaceRoot.Update(gameTime);
            player.Update(gameTime);

            // Only need to check collisions on world space Nodes.
            var allNodes = worldSpaceRoot.GetSelfAndAllChildren();
            var enabledCollisionNodes = allNodes.OfType<ICollisionNode>().Where(c => c.CollisionsEnabled);

            CheckPlayerCollisions(player, enabledCollisionNodes);
        }

        /// <summary>
        /// Checks if the given PlayerController collides with anything in the given list of ICollisionNodes. If there is
        /// a collision then notify the PlayerController of the collision.
        /// </summary>
        /// <param name="playerController">The PlayerController to check for collisions against.</param>
        /// <param name="collisionNodes">The list of ICollisionNodes that could be colliding with the PlayerController.</param>
        private void CheckPlayerCollisions(PlayerController playerController, IEnumerable<ICollisionNode> collisionNodes)
        {
            foreach (ICollisionNode collisionNode in collisionNodes) 
            {
                if (player.CollisionShape.Intersects(collisionNode.CollisionShape))
                {
                    player.OnCollision(collisionNode);
                }
            }
        }
    }
}
