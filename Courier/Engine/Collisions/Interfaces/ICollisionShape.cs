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
    /// Interface to be implemented by all Collision Shapes. Collision Shapes in this game are only used to detect intersections and not for creating collision simulations.
    /// </summary>
    public interface ICollisionShape
    {
        /// <summary>
        /// Returns true if the given collisionShape intersects with this one.
        /// </summary>
        /// <param name="collisionShape"></param>
        public bool Intersects(ICollisionShape collisionShape);
        /// <summary>
        /// Distance from the origin to the bottom of the collision shape. 
        /// This number should be larger than the value for GetTop() since down is positive and up is negative in world space.
        /// </summary>
        public float GetBottom();
        /// <summary>
        /// Distance from the origin to the top of the collision shape.
        /// This number should be smaller than the value for GetBottom() since down is positive and up is negative in world space.
        /// </summary>
        public float GetTop();
        /// <summary>
        /// Distance from the origin to the leftmost point of the collision shape.
        /// </summary>
        public float GetLeft();
        /// <summary>
        /// Distance from the origin to the rightmost point of the collision shape.
        /// </summary>
        public float GetRight();
    }
}
