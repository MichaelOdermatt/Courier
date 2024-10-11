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
    /// Base class for all spatial game objects.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// List of child Nodes.
        /// </summary>
        protected readonly List<Node> Children = new List<Node>();

        /// <summary>
        /// Position of the game object relative to it's parent Node.
        /// </summary>
        public Vector2 Position { get; set; } = Vector2.Zero;

        public Node(List<Node> children)
        {
            Children = children;
        }

        public Node() { }

        /// <summary>
        /// To be called in the overriding function. Draws all child Nodes.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch instance initialized in Game1.</param>
        /// <param name="assetManager">The AssetManager instance initialized in Game1.</param>
        /// <param name="parentPosition">The position of the parent Node. Used to draw Nodes relative to their parent.</param>
        public virtual void Draw(SpriteBatch spriteBatch, AssetManager assetManager, Vector2 parentPosition)
        {
            Children.ForEach(n => n.Draw(spriteBatch, assetManager, parentPosition + Position));
        }

        /// <summary>
        /// To be called in the overriding function. Calls Update on all child Nodes.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public virtual void Update(GameTime gameTime)
        {
            Children.ForEach(n => n.Update(gameTime));
        }

        /// <summary>
        /// Returns a list of Nodes that includes itself and all its Children.
        /// </summary>
        public List<Node> GetSelfAndAllChildren()
        {
            var nodes = new List<Node>();
            var stack = new Stack<Node>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                nodes.Add(current);

                foreach (var child in current.Children)
                {
                    stack.Push(child);
                }
            }

            return nodes;
        }
    }
}
