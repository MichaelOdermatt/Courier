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

        // TODO if I'm going to use the vector2 comparer maybe I should store it elsewhere
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

                Vector2? leftPoint = null;
                Vector2? rightPoint = null;

                // TODO is there a better way to get the point index from the binary search result?

                if (index < -1 && index > (collisionSB.Points.Length * -1) - 1)
                {
                    var rightIndex = (index * -1) - 1;
                    var leftIndex = rightIndex - 1;

                    leftPoint = collisionSB.Points[leftIndex];
                    rightPoint = collisionSB.Points[rightIndex];
                }

                if (leftPoint == null || rightPoint == null)
                {
                    return false;
                }

                Vector2 lineSegmentVector = rightPoint.Value - leftPoint.Value;
                Vector2 startPosToSphereCenter = Position - leftPoint.Value;

                // Check if the sphere and line intersect

                // Find the closest point to the sphere center along the lineSegmentVector.
                float closestPointAsFloat = Vector2.Dot(startPosToSphereCenter, lineSegmentVector) / lineSegmentVector.LengthSquared();

                closestPointAsFloat = Math.Clamp(closestPointAsFloat, 0 , 1);

                Vector2 closestPoint = leftPoint.Value + closestPointAsFloat * lineSegmentVector;

                float distanceTo = (closestPoint - Position).LengthSquared();

                // True if the distance between the closest point on the line to the sphere is less than the radius, or if the sphere's origin is below the closest point.
                return closestPoint.Y <= Position.Y || distanceTo <= radius * radius;
            }

            throw new NotImplementedException();
        }
    }
}
