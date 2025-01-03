using Courier.Engine.Nodes.Interfaces;
using Courier.Engine.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Nodes
{
    /// <summary>
    /// Base class for all spatial game objects.
    /// </summary>
    public class Node : INode
    {
        protected readonly Node parent;

        public List<Node> Children { get; private set; } = new List<Node>();

        public float LocalRotation { get; set; } = 0f;

        public float GlobalRotation { get => parent != null ? parent.LocalRotation + LocalRotation : LocalRotation; }

        public Vector2 LocalPosition { get; set; } = Vector2.Zero;

        public Vector2 GlobalPosition { get => parent != null ? parent.LocalPosition + LocalPosition : LocalPosition; }

        public Node(Node parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// To be called in the overriding function. Draws all child Nodes.
        /// </summary>
        /// <param name="spriteRenderer">An instance of the SpriteRenderer object used to draw all game sprites.</param>
        public virtual void Draw(SpriteRenderer spriteRenderer)
        {
            Children.ForEach(n => n.Draw(spriteRenderer));
        }

        /// <summary>
        /// To be called in the overriding function. Calls Update on all child Nodes.
        /// </summary>
        /// <param name="gameTime">The GameTime object to be used in update calculations.</param>
        public virtual void Update(GameTime gameTime)
        {
            Children.ForEach(n => n.Update(gameTime));
        }

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
