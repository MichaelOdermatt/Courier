using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions.Interfaces
{
    /// <summary>
    /// Interface to be implemented by all Collision Shapes. Collision Shapes are only used to detect intersections and not for creating collision simulations.
    /// </summary>
    public interface ICollisionShape
    {
        /// <summary>
        /// Returns a CollisionBoundingRect of the smallest rectangle that contains the collision shape.
        /// </summary>
        /// <param name="transformMatrix">The transform matrix to apply to the shape before returning the bounding rect.</param>
        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix);
    }
}
