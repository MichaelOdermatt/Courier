using Auios.QuadTree;
using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Nodes.Interfaces;
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
    public interface ICollisionNode : INode
    {
        ICollisionShape CollisionShape { get; }

        /// <summary>
        /// The type of the CollisionNode. Player, Ground, Bullet... etc
        /// </summary>
        CollisionNodeType CollisionNodeType { get; }

        /// <summary>
        /// The types of CollisionNodes that this CollisionNode is checking for collisions with.
        /// </summary>
        CollisionNodeType[] CollisionNodeTypeMask { get; }

        /// <summary>
        /// Boolean value representing if collision checking should be performed on this CollisionNode.
        /// </summary>
        bool CollisionsEnabled { get; }

        public event EventHandler<CollisionEventArgs> OnCollision;

        /// <summary>
        /// Returns true if the given CollisionNodeType is masked by this CollisionNode. Otherwise false.
        /// </summary>
        public bool IsMaskingNodeType(CollisionNodeType nodeType);

        /// <summary>
        /// Called when notifying the ICollisionNode of a collision with another ICollisionNode.
        /// </summary>
        /// <param name="e">The ICollisionNode that collided with this ICollisionNode colliding Node.</param>
        public void Collide(ICollisionNode e);

        /// <summary>
        /// Returns a QuadTreeRect which represents the ICollisionNode's width, height, and position in world space.
        /// </summary>
        public QuadTreeRect GetQuadTreeRect();

        /// <summary>
        /// Returns a CollisionBoundingRect of the smallest rectangle that fits the ICollisionNode, with position and rotation transforms applied.
        /// </summary>
        public CollisionBoundingRect GetCollisionBoundingRect();
    }
}
