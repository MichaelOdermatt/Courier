using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
{
    public class CollisionSphere : ICollisionShape
    {
        private float radius;
        /// <summary>
        /// The Node which the CollisionSphere is attached to.
        /// </summary>
        private readonly Node instantiatingNode;
        private Vector2 GlobalPosition { get => instantiatingNode.GlobalPosition; }

        public CollisionSphere(Node instantiatingNode, float radius)
        {
            this.radius = radius;
            this.instantiatingNode = instantiatingNode;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSegmentedBoundry collisionSB && collisionSB.Direction == SegmentedBoundryDirection.Down)
            {
                return IntersectsWithCollisionSegmentedBoundry(collisionSB);
            } else if (collisionShape is CollisionSphere collisionSphere)
            {
                return IntersectsWithCollisionSphere(collisionSphere);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionSegmentedBoundry. Otherwise false.
        /// </summary>
        private bool IntersectsWithCollisionSegmentedBoundry(CollisionSegmentedBoundry collisionSB)
        {
            // Find the line segment below the player.

            var index = Array.BinarySearch(collisionSB.Points, GlobalPosition, CollisionSegmentedBoundry.Vector2Comparer);

            var rightIndex = (index * -1) - 1;
            var leftIndex = rightIndex - 1;

            // If the left and right index are out of the range of they array, the player is not above any line segment.
            if (rightIndex > collisionSB.Points.Length - 1 || leftIndex < 0)
            {
                return false;
            }

            Vector2 leftPoint = collisionSB.Points[leftIndex];
            Vector2 rightPoint = collisionSB.Points[rightIndex];

            Vector2 lineSegmentVector = rightPoint - leftPoint;
            Vector2 startPosToSphereCenter = GlobalPosition - leftPoint;

            // Check if the sphere and line intersect

            // Find the closest point to the sphere center along the lineSegmentVector.
            float closestPointAsFloat = Vector2.Dot(startPosToSphereCenter, lineSegmentVector) / lineSegmentVector.LengthSquared();

            closestPointAsFloat = Math.Clamp(closestPointAsFloat, 0 , 1);

            Vector2 closestPoint = leftPoint + closestPointAsFloat * lineSegmentVector;

            float distanceTo = (closestPoint - GlobalPosition).LengthSquared();

            // True if the distance between the closest point on the line to the sphere is less than the radius, or if the sphere's origin is below the closest point.
            return closestPoint.Y <= GlobalPosition.Y || distanceTo <= radius * radius;
        }

        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionSphere. Otherwise false.
        /// </summary>
        private bool IntersectsWithCollisionSphere(CollisionSphere collisionSphere)
        {
            var distanceTo = (collisionSphere.GlobalPosition - GlobalPosition).Length();
            return distanceTo <= radius + collisionSphere.radius;
        }
    }
}
