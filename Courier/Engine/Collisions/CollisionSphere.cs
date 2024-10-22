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
        public Node Parent { get; set; }
        private Vector2 Position { get => Parent.GlobalPosition; }
        /// <summary>
        /// A Comparer object for Vector2 that is used with binary search to detect which Vector2 points the player is closest to.
        /// </summary>
        private readonly Comparer<Vector2> Vector2Comparer = Comparer<Vector2>.Create((a, b) => a.X > b.X ? 1 : a.X < b.X ? -1 : 1);

        public CollisionSphere(float radius)
        {
            this.radius = radius;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSegmentedBoundry collisionSB && collisionSB.Direction == SegmentedBoundryDirection.Down)
            {
                // Find the line segment below the player.

                var index = Array.BinarySearch(collisionSB.Points, Position, Vector2Comparer);

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
                Vector2 startPosToSphereCenter = Position - leftPoint;

                // Check if the sphere and line intersect

                // Find the closest point to the sphere center along the lineSegmentVector.
                float closestPointAsFloat = Vector2.Dot(startPosToSphereCenter, lineSegmentVector) / lineSegmentVector.LengthSquared();

                closestPointAsFloat = Math.Clamp(closestPointAsFloat, 0 , 1);

                Vector2 closestPoint = leftPoint + closestPointAsFloat * lineSegmentVector;

                float distanceTo = (closestPoint - Position).LengthSquared();

                // True if the distance between the closest point on the line to the sphere is less than the radius, or if the sphere's origin is below the closest point.
                return closestPoint.Y <= Position.Y || distanceTo <= radius * radius;
            }

            throw new NotImplementedException();
        }
    }
}
