using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions.Interfaces
{
    /// <summary>
    /// Nodes that implement this interface are ones that have collision checks and can collide with other Nodes.
    /// </summary>
    public interface ICollisionNode
    {
        ICollisionShape CollisionShape { get; }

        /// <summary>
        /// The type of the CollisionNode. Player, Ground, Bullet... etc
        /// </summary>
        CollisionNodeType CollisionNodeType { get; }

        /// <summary>
        /// Boolean value representing if collision checking should be performed on this CollisionNode.
        /// </summary>
        bool CollisionsEnabled { get; }

        public event EventHandler<CollisionEventArgs> OnCollision;

        /// <summary>
        /// Called when notifying the ICollisionNode of a collision with another ICollisionNode.
        /// </summary>
        /// <param name="e">The ICollisionNode that collided with this ICollisionNode colliding Node.</param>
        public void Collide(ICollisionNode e);
    }
}
