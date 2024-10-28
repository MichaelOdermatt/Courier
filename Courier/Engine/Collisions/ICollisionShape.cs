using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
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
    }
}
