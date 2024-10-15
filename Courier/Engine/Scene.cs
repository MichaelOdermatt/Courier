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
        /// Calls Initialize recursively on the root node until all it's children are Initialized;
        /// </summary>
        public void Initialize()
        {
            root.Initialize();
            player.Initialize();
        }

        /// <summary>
        /// Calls Draw recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance initialized in Game1.</param>
        /// <param name="assetManager">The AssetManager instance initialized in Game1.</param>
        public void Draw(SpriteBatch spriteBatch, AssetManager assetManager)
        {
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            root.Draw(spriteBatch, assetManager);
            player.Draw(spriteBatch, assetManager);
        }

        /// <summary>
        /// Calls Update recursively on the root node until all it's children are rendered.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public void Update(GameTime gameTime)
        {
            root.Update(gameTime);
            player.Update(gameTime);

            var allNodes = root.GetSelfAndAllChildren();
            var collissionNodes = allNodes.OfType<ICollisionNode>();

            CheckPlayerCollisions(player, collissionNodes);
        }

        /// <summary>
        /// Checks if the given PlayerController collides with anything in the given list of ICollisionNodes. If there is
        /// a collision then notify the PlayerController of the collision.
        /// </summary>
        /// <param name="playerController">The PlayerController to check for collisions against.</param>
        /// <param name="collisionNodes">The list of ICollisionNodes that could be colliding with the PlayerController.</param>
        public void CheckPlayerCollisions(PlayerController playerController, IEnumerable<ICollisionNode> collisionNodes)
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
