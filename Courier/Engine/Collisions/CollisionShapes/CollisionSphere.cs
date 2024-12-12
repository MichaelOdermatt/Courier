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
    public class CollisionSphere : ICollisionShape
    {
        public float radius;
        /// <summary>
        /// The Node which the CollisionSphere is attached to.
        /// </summary>
        private readonly Node instantiatingNode;
        public Vector2 GlobalPosition { get => instantiatingNode.GlobalPosition; }

        public CollisionSphere(Node instantiatingNode, float radius)
        {
            this.radius = radius;
            this.instantiatingNode = instantiatingNode;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSegmentedBoundry collisionSB && collisionSB.Direction == CollisionBoundryDirection.Down)
            {
                return CheckIntersections.SphereIntersectsWithSegmentedBoundry(this, collisionSB);
            }
            else if (collisionShape is CollisionSphere collisionSphere)
            {
                return CheckIntersections.SphereIntersectsWithSphere(this, collisionSphere);
            }
            else if (collisionShape is CollisionLineBoundry collisionLB && collisionLB.Direction == CollisionBoundryDirection.Right)
            {
                return CheckIntersections.SphereIntersectsWithLineBoundry(this, collisionLB);
            }

            throw new NotImplementedException();
        }

        public float GetBottom() => radius;

        public float GetTop() => -radius;

        public float GetLeft() => -radius;

        public float GetRight() => radius;
    }
}
