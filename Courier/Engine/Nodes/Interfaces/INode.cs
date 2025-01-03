using System.Collections.Generic;
using Courier.Engine.Render;
using Microsoft.Xna.Framework;

namespace Courier.Engine.Nodes.Interfaces 
{
    // TODO use INode in places rather than Node
    public interface INode
    {
        /// <summary>
        /// List of child Nodes.
        /// </summary>
        public List<Node> Children { get; }

        /// <summary>
        /// Rotation (in radians) of the game object relative to it's parent Node.
        /// </summary>
        public float LocalRotation { get; }

        /// <summary>
        /// Rotation (in radians) of the game object relative to the root Node.
        /// </summary>
        public float GlobalRotation { get; }

        /// <summary>
        /// Position of the game object relative to it's parent Node.
        /// </summary>
        public Vector2 LocalPosition { get; }

        /// <summary>
        /// Position of the game object relative to the root Node.
        /// </summary>
        public Vector2 GlobalPosition { get; }

        public void Draw(SpriteRenderer spriteRenderer);

        public void Update(GameTime gameTime);

        /// <summary>
        /// Returns a list of Nodes that includes itself and all its Children.
        /// </summary>
        public List<Node> GetSelfAndAllChildren();
    }
}