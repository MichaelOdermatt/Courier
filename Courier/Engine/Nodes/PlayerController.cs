using Courier.Engine.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Nodes
{
    /// <summary>
    /// Base class for handling all non implementation specific player code.
    /// </summary>
    public class PlayerController : Node, ICollisionNode
    {
        /// <summary>
        /// When you want to move a PlayerController update it's Velocity instead of it's Position.
        /// </summary>
        public Vector2 Velocity { get; set; }

        public ICollisionShape CollisionShape { get; protected set; }
        public bool CollisionsEnabled { get; set; } = true;

        public PlayerController(Node parent) : base(parent)
        {
        }

        /// <summary>
        /// This function updates the PlayerController's position based on it's velocity.
        /// </summary>
        protected void ApplyVelocity()
        {
            LocalPosition += Velocity;
        }

        /// <summary>
        /// Function to call when notifying the PlayerController of a collision with another ICollisionNode
        /// </summary>
        /// <param name="collisionNode">The object which has collided with the player.</param>
        public virtual void OnCollision(ICollisionNode collisionNode)
        {
        }
    }
}
