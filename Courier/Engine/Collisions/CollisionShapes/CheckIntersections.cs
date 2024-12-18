using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public static class CheckIntersections
    {
        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionSegmentedBoundry. Otherwise false.
        /// </summary>
        public static bool SphereIntersectsWithSegmentedBoundry(CollisionSphere collisionSphere, CollisionSegmentedBoundry collisionSB)
        {
            // Find the line segment below the player.

            var index = Array.BinarySearch(collisionSB.Points, collisionSphere.GlobalPosition, CollisionSegmentedBoundry.Vector2Comparer);

            var rightIndex = index * -1 - 1;
            var leftIndex = rightIndex - 1;

            // If the left and right index are out of the range of they array, the player is not above any line segment.
            if (rightIndex > collisionSB.Points.Length - 1 || leftIndex < 0)
            {
                return false;
            }

            Vector2 leftPoint = collisionSB.Points[leftIndex];
            Vector2 rightPoint = collisionSB.Points[rightIndex];

            Vector2 lineSegmentVector = rightPoint - leftPoint;
            Vector2 startPosToSphereCenter = collisionSphere.GlobalPosition - leftPoint;

            // Check if the sphere and line intersect

            // Find the closest point to the sphere center along the lineSegmentVector.
            float closestPointAsFloat = Vector2.Dot(startPosToSphereCenter, lineSegmentVector) / lineSegmentVector.LengthSquared();

            closestPointAsFloat = Math.Clamp(closestPointAsFloat, 0, 1);

            Vector2 closestPoint = leftPoint + closestPointAsFloat * lineSegmentVector;

            float distanceTo = (closestPoint - collisionSphere.GlobalPosition).LengthSquared();

            var radius = collisionSphere.radius;
            // True if the distance between the closest point on the line to the sphere is less than the radius, or if the sphere's origin is below the closest point.
            return closestPoint.Y <= collisionSphere.GlobalPosition.Y || distanceTo <= radius * radius;
        }

        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionSphere. Otherwise false.
        /// </summary>
        public static bool SphereIntersectsWithLineBoundry(CollisionSphere collisionSphere, CollisionLineBoundry collisionLB)
        {
            var distanceTo = collisionLB.GlobalPosition.X - collisionSphere.GlobalPosition.X;
            return distanceTo <= collisionSphere.radius;
        }

        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionRectangle. Otherwise false.
        /// </summary>
        public static bool SphereIntersectsWithRectangle(CollisionSphere collisionSphere, CollisionRectangle collisionRectangle)
        {
            var distanceToX = Math.Abs(collisionSphere.GlobalPosition.X - collisionRectangle.GlobalPosition.X);
            var distanceToY = Math.Abs(collisionSphere.GlobalPosition.Y - collisionRectangle.GlobalPosition.Y);
            var isIntersectingX = distanceToX <= collisionSphere.radius + collisionRectangle.Width * 0.5f;
            var isIntersectingY = distanceToY <= collisionSphere.radius + collisionRectangle.Height * 0.5f;
            return isIntersectingX && isIntersectingY;
        }

        /// <summary>
        /// Returns true if this CollisionSphere intersects with the given CollisionSphere. Otherwise false.
        /// </summary>
        public static bool SphereIntersectsWithSphere(CollisionSphere collisionSphere1, CollisionSphere collisionSphere2)
        {
            var distanceTo = (collisionSphere1.GlobalPosition - collisionSphere2.GlobalPosition).Length();
            return distanceTo <= collisionSphere1.radius + collisionSphere2.radius;
        }
    }
}
