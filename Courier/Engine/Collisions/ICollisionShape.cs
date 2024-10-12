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
        // TODO maybe this shouldn't be called parent, since the ICollision shape is actually not a child of the node that instantiates it.
        Node Parent { get; set; }

        /// <summary>
        /// Returns true if the given collisionShape intersects.
        /// </summary>
        /// <param name="collisionShape"></param>
        public bool Intersects(ICollisionShape collisionShape);
    }
}
