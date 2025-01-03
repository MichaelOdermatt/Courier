using Courier.Engine.Collisions.Interfaces;
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
        /// Returns true if the given two ICollisionNodes intersect with eachother. Otherwise false.
        /// </summary>
        /// <exception cref="NotImplementedException">Throws NotImplementedException if there is no support for comparing the CollisionShapes of the given IColliisonNodes.</exception>
        public static bool CheckIntersectionsOnNodes(ICollisionNode collisionNode1, ICollisionNode collisionNode2)
        {
            // TODO simpler way to do this?
            switch (collisionNode1.CollisionShape) {
                case CollisionSphere collisionSphere:
                    switch (collisionNode2.CollisionShape) {
                        case CollisionSphere collisionSphere2:
                            return SphereIntersectsWithSphere(collisionNode1, collisionNode2, collisionSphere, collisionSphere2);
                        case CollisionRectangle collisionRectangle:
                            return SphereIntersectsWithRectangle(collisionNode1, collisionNode2, collisionSphere, collisionRectangle);
                        case CollisionRaycast collisionRaycast:
                            return SphereIntersectsWithRaycast(collisionNode1, collisionNode2, collisionSphere, collisionRaycast);
                        case CollisionLineBoundry:
                            return SphereIntersectsWithLineBoundry(collisionNode1, collisionNode2, collisionSphere);
                        case CollisionSegmentedBoundry collisionSB:
                            return SphereIntersectsWithSegmentedBoundry(collisionNode1, collisionSphere, collisionSB);
                        default:
                            throw new NotImplementedException();
                    }
                case CollisionRectangle collisionRectangle:
                    switch (collisionNode2.CollisionShape) {
                        case CollisionSphere collisionSphere:
                            return SphereIntersectsWithRectangle(collisionNode2, collisionNode1, collisionSphere, collisionRectangle);
                        default:
                            throw new NotImplementedException();
                    }
                case CollisionRaycast collisionRaycast:
                    switch (collisionNode2.CollisionShape) {
                        case CollisionSphere collisionSphere:
                            return SphereIntersectsWithRaycast(collisionNode2, collisionNode1, collisionSphere, collisionRaycast);
                        default:
                            throw new NotImplementedException();
                    }
                case CollisionLineBoundry:
                    switch (collisionNode2.CollisionShape) {
                        case CollisionSphere collisionSphere:
                            return SphereIntersectsWithLineBoundry(collisionNode2, collisionNode1, collisionSphere);
                        default:
                            throw new NotImplementedException();
                    }
                case CollisionSegmentedBoundry collisionSB:
                    switch (collisionNode2.CollisionShape) {
                        case CollisionSphere collisionSphere:
                            return SphereIntersectsWithSegmentedBoundry(collisionNode2, collisionSphere, collisionSB);
                        default:
                            throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }

        }

        /// <summary>
        /// Returns true if the given collision sphere intersects with the given collision segmented boundry. Otherwise false.
        /// </summary>
        /// <param name="collisionSphere">The ICollisionNode for the sphere.</param>
        /// <param name="collisionSphereShape">The shape used by the sphere's ICollisionNode.</param>
        /// <param name="collisionSBShape">The shape used by the segmented boundry's ICollisionNode.</param>
        private static bool SphereIntersectsWithSegmentedBoundry(
            ICollisionNode collisionSphere, 
            CollisionSphere collisionSphereShape, 
            CollisionSegmentedBoundry collisionSBShape
        )
        {
            // Find the line segment below the player.
            var index = Array.BinarySearch(collisionSBShape.Points, collisionSphere.GlobalPosition, CollisionSegmentedBoundry.Vector2Comparer);

            var rightIndex = index * -1 - 1;
            var leftIndex = rightIndex - 1;

            // If the left and right index are out of the range of they array, the player is not above any line segment.
            if (rightIndex > collisionSBShape.Points.Length - 1 || leftIndex < 0)
            {
                return false;
            }

            Vector2 leftPoint = collisionSBShape.Points[leftIndex];
            Vector2 rightPoint = collisionSBShape.Points[rightIndex];

            Vector2 lineSegmentVector = rightPoint - leftPoint;
            Vector2 startPosToSphereCenter = collisionSphere.GlobalPosition - leftPoint;

            // Check if the sphere and line intersect

            // Find the closest point to the sphere center along the lineSegmentVector.
            float closestPointAsFloat = Vector2.Dot(startPosToSphereCenter, lineSegmentVector) / lineSegmentVector.LengthSquared();

            closestPointAsFloat = Math.Clamp(closestPointAsFloat, 0, 1);

            Vector2 closestPoint = leftPoint + closestPointAsFloat * lineSegmentVector;

            float distanceTo = (closestPoint - collisionSphere.GlobalPosition).LengthSquared();

            var radius = collisionSphereShape.Radius;
            // True if the distance between the closest point on the line to the sphere is less than the radius, or if the sphere's origin is below the closest point.
            return closestPoint.Y <= collisionSphere.GlobalPosition.Y || distanceTo <= radius * radius;
        }

        /// <summary>
        /// Returns true if the given collision sphere intersects with the given collision line boundry. Otherwise false.
        /// </summary>
        /// <param name="collisionSphere">The ICollisionNode for the sphere.</param>
        /// <param name="collisionLB">The shape used by the line boundry's ICollisionNode.</param>
        /// <param name="collisionSphereShape">The shape used by the segmented boundry's ICollisionNode.</param>
        private static bool SphereIntersectsWithLineBoundry(
            ICollisionNode collisionSphere, 
            ICollisionNode collisionLB, 
            CollisionSphere collisionSphereShape
        )
        {
            var distanceTo = collisionLB.GlobalPosition.X - collisionSphere.GlobalPosition.X;
            return distanceTo <= collisionSphereShape.Radius;
        }

        /// <summary>
        /// Returns true if the given collision sphere intersects with the given collision rectangle. Otherwise false.
        /// </summary>
        /// <param name="collisionSphere">The ICollisionNode for the sphere.</param>
        /// <param name="collisionRectangle">The ICollisionNode for the rectangle.</param>
        /// <param name="collisionSphereShape">The shape used by the sphere's ICollisionNode.</param>
        /// <param name="collisionRectangleShape">The shape used by the rectangle's ICollisionNode.</param>
        private static bool SphereIntersectsWithRectangle(
            ICollisionNode collisionSphere, 
            ICollisionNode collisionRectangle, 
            CollisionSphere collisionSphereShape, 
            CollisionRectangle collisionRectangleShape
        )
        {
            // TODO need to update this calculation to account for a rotated rectangle
            var distanceToX = Math.Abs(collisionSphere.GlobalPosition.X - collisionRectangle.GlobalPosition.X);
            var distanceToY = Math.Abs(collisionSphere.GlobalPosition.Y - collisionRectangle.GlobalPosition.Y);
            var isIntersectingX = distanceToX <= collisionSphereShape.Radius + collisionRectangleShape.Width * 0.5f;
            var isIntersectingY = distanceToY <= collisionSphereShape.Radius + collisionRectangleShape.Height * 0.5f;
            return isIntersectingX && isIntersectingY;
        }

        /// <summary>
        /// Returns true if the given collision sphere intersects with the given collision raycast. Otherwise false.
        /// </summary>
        /// <param name="collisionSphere">The ICollisionNode for the sphere.</param>
        /// <param name="collisionRaycast">The ICollisionNode for the raycast.</param>
        /// <param name="collisionSphereShape">The shape used by the sphere's ICollisionNode.</param>
        /// <param name="collisionRaycastShape">The shape used by the raycast's ICollisionNode.</param>
        private static bool SphereIntersectsWithRaycast(
            ICollisionNode collisionSphere, 
            ICollisionNode collisionRaycast, 
            CollisionSphere collisionSphereShape, 
            CollisionRaycast collisionRaycastShape
        )
        {
            var rayDirection = new Vector2(MathF.Cos(collisionRaycast.GlobalRotation), MathF.Sin(collisionRaycast.GlobalRotation));
            rayDirection.Normalize();

            var rayToSphere = collisionSphere.GlobalPosition - collisionRaycast.GlobalPosition;

            var projection = Vector2.Dot(rayToSphere, rayDirection);

            if (projection < 0) {
                return false;
            }

            // Since the raycast has a length, get the shortest of the projection and the raycast length.
            var length = projection > collisionRaycastShape.Length ? collisionRaycastShape.Length : projection;

            var closestPointOnRay = collisionRaycast.GlobalPosition + length * rayDirection;

            var distanceToSphere = Vector2.Distance(closestPointOnRay, collisionSphere.GlobalPosition);

            return distanceToSphere <= collisionSphereShape.Radius;
        }

        /// <summary>
        /// Returns true if the first given collision sphere intersects with the second given collision sphere. Otherwise false.
        /// </summary>
        /// <param name="collisionSphere1">The ICollisionNode for the first sphere.</param>
        /// <param name="collisionSphere2">The ICollisionNode for the second sphere.</param>
        /// <param name="collisionSphereShape">The shape used by the frist sphere's ICollisionNode.</param>
        /// <param name="collisionSphereShape">The shape used by the second sphere's ICollisionNode.</param>
        private static bool SphereIntersectsWithSphere(
            ICollisionNode collisionSphere1, 
            ICollisionNode collisionSphere2, 
            CollisionSphere collisionSphereShape1, 
            CollisionSphere collisionSphereShape2
        )
        {
            var distanceTo = (collisionSphere1.GlobalPosition - collisionSphere2.GlobalPosition).Length();
            return distanceTo <= collisionSphereShape1.Radius + collisionSphereShape2.Radius;
        }
    }
}
