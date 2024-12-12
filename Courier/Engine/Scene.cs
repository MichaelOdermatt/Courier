using Auios.QuadTree;
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

        /// <summary>
        /// QuadTree used for detecting intersections between collision nodes.
        /// </summary>
        private readonly QuadTree<ICollisionNode> quadTree;

        public Scene(Camera2D camera)
        {
            this.camera = camera;
            // Collisions will be checked in a 10000 by 10000 square starting at the level's origin.
            quadTree = new QuadTree<ICollisionNode>(10000, 10000, new CollisionNodeQuadTreeBounds());
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
        /// <param name="collisionNodes">The list all CollisionNodes in the Scene.</param>
        private void CheckSceneCollisions(IEnumerable<ICollisionNode> collisionNodes)
        {
            // Clear all the nodes from the previous Update call.
            quadTree.Clear();
            // Add all the node from the current Update call.
            quadTree.InsertRange(collisionNodes);

            foreach (var node in collisionNodes)
            {
                // Get the search area to check in the QuadTree.
                var searchArea = node.GetQuadTreeRect();
                // Get any nodes that are in the search area in the QuadTree.
                var collidingNodes = quadTree.FindObjects(searchArea);
                foreach (var collidingNode in collisionNodes)
                {
                    var firstCollisionNodeMasksSecondNode = node.IsMaskingNodeType(collidingNode.CollisionNodeType);
                    var secondCollisionNodeMasksFirstNode = collidingNode.IsMaskingNodeType(node.CollisionNodeType);

                    // If either of the Nodes mask the other, and they are colliding, notify them.
                    if ((firstCollisionNodeMasksSecondNode || secondCollisionNodeMasksFirstNode) &&
                        node.CollisionShape.Intersects(collidingNode.CollisionShape)
                    )
                    {
                        if (firstCollisionNodeMasksSecondNode)
                        {
                            node.Collide(collidingNode);
                        }
                        if (secondCollisionNodeMasksFirstNode)
                        {
                            collidingNode.Collide(node);
                        }
                    }
                }
            }
        }
    }
}
