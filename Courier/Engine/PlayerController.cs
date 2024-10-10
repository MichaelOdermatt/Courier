using Courier.Content;
using Courier.Engine.Collisions;
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
    /// Base class for handling all non implementation specific player code.
    /// </summary>
    public class PlayerController : Node, ICollisionNode
    {
        /// <summary>
        /// When you want to move a PlayerController update it's Velocity instead of it's Position.
        /// </summary>
        public Vector2 Velocity { get; set; }

        public ICollisionShape CollisionShape { get; set; }

        public PlayerController(ICollisionShape collisionShape)
        {
            CollisionShape = collisionShape;
        }

        /// <summary>
        /// This function updates the PlayerController's position based on it's velocity.
        /// </summary>
        protected void ApplyVelocity()
        {
            Position += Velocity;
        }
    }
}
