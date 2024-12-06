using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public class CollisionLineBoundry : ICollisionShape
    {
        /// <summary>
        /// The Node which the CollisionLineBoundry is attached to.
        /// </summary>
        private readonly Node instantiatingNode;

        public Vector2 GlobalPosition { get => instantiatingNode.GlobalPosition; }

        /// <summary>
        /// The direction in which the collision shape should bound off.
        /// </summary>
        public CollisionBoundryDirection Direction { get; private set; }

        public CollisionLineBoundry(Node instantiatingNode, CollisionBoundryDirection direction)
        {
            this.instantiatingNode = instantiatingNode;
            Direction = direction;
        }

        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSphere collisionSphere)
            {
                return CheckIntersections.SphereIntersectsWithLineBoundry(collisionSphere, this);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
