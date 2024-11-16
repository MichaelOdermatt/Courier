using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
{
    /// <summary>
    /// Nodes that implement this interface are ones that have collision checks and can collide with other Nodes.
    /// </summary>
    public interface ICollisionNode
    {
        ICollisionShape CollisionShape { get; }

        /// <summary>
        /// Boolean value representing if collision checking should be performed on this CollisionNode.
        /// </summary>
        bool CollisionsEnabled { get; }

        /// <summary>
        /// Called when notifying the ICollisionNode of a collision with another ICollisionNode
        /// </summary>
        /// <param name="collisionNode">The object which has collided with this Node.</param>
        public void OnCollision(ICollisionNode collisionNode);
    }
}
