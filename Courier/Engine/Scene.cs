using Courier.Engine.Collisions;
using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Courier.Engine.Render;
using Courier.Game;
using Courier.Game.PlayerCode;
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
        /// <param name="spriteRenderer">An instance of the SpriteRenderer object used to draw all game sprites.</param>
        public void Draw(SpriteRenderer spriteRenderer)
        {
            // Since root is the top level node, pass Vector2.Zero as its parent position.
            screenSpaceRoot.Draw(spriteRenderer);
            worldSpaceRoot.Draw(spriteRenderer);
        }

        /// <summary>
        /// Calls Update recursively on the root node until all it's children are updated.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public void Update(GameTime gameTime)
        {
            screenSpaceRoot.Update(gameTime);
            worldSpaceRoot.Update(gameTime);

            var allNodes = worldSpaceRoot.GetSelfAndAllChildren();
            var enabledCollisionNodes = allNodes.OfType<ICollisionNode>().Where(c => c.CollisionsEnabled);

            CheckSceneCollisions(enabledCollisionNodes);
        }

        /// <summary>
        /// Checks for collisions between the given list of ICollisionNodes.
        /// </summary>
        /// <param name="collisionNodes">The list of ICollisionNodes that could be colliding with the PlayerController.</param>
        private void CheckSceneCollisions(IEnumerable<ICollisionNode> collisionNodes)
        {
            for (int i = 0; i < collisionNodes.Count() - 1; i++)
            {
                for (int j = i + 1; j < collisionNodes.Count(); j++)
                {
                    var firstCollisionNode = collisionNodes.ElementAt(i);
                    var secondCollisionNode = collisionNodes.ElementAt(j);
                    // If the collision nodes intersect, notify them.
                    if (firstCollisionNode.CollisionShape.Intersects(secondCollisionNode.CollisionShape))
                    {
                        firstCollisionNode.Collide(secondCollisionNode);
                        secondCollisionNode.Collide(firstCollisionNode);
                    }
                }
            }
        }
    }
}
