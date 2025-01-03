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
    /// <summary>
    /// A CollisionShape representing a level boundry that consists of a segmented line rather than a straight line. 
    /// </summary>
    public class CollisionSegmentedBoundry : ICollisionShape
    {
        /// <summary>
        /// Amount of additional collision space on the opposing side of the boundry.
        /// </summary>
        private const float boundryBufferZone = 250f;
        private readonly float highestPointYValue;
        private readonly float lowestPointYValue;
        private readonly float rightmostPointXValue;
        private readonly float leftmostPointXValue;

        /// <summary>
        /// The points that make up the segmented line.
        /// </summary>
        public readonly Vector2[] Points;

        /// <summary>
        /// The direction in which the collision shape should bound off.
        /// </summary>
        public readonly CollisionBoundryDirection Direction;

        /// <summary>
        /// A Comparer object for Vector2 that is used with binary search to detect which Vector2 points the player is closest to.
        /// </summary>
        public static Comparer<Vector2> Vector2Comparer { get; } = Comparer<Vector2>.Create((a, b) => a.X > b.X ? 1 : a.X < b.X ? -1 : 1);

        public CollisionSegmentedBoundry(Vector2[] points, CollisionBoundryDirection direction)
        {
            Points = points;
            Direction = direction;

            highestPointYValue = float.MinValue;
            lowestPointYValue = float.MaxValue;
            rightmostPointXValue = float.MinValue;
            leftmostPointXValue = float.MaxValue;
            foreach (var point in points)
            {
                highestPointYValue = point.Y > highestPointYValue ? point.Y : highestPointYValue;
                lowestPointYValue = point.Y < lowestPointYValue ? point.Y : lowestPointYValue;
                rightmostPointXValue = point.X > rightmostPointXValue ? point.X : rightmostPointXValue;
                leftmostPointXValue = point.X < leftmostPointXValue ? point.X : leftmostPointXValue;
            }
        }

        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix)
        {
            // The transform matrix does not not get applied in this case because we don't want segmented boundries to be moved or rotated.
            float bottomValue;
            switch (Direction) {
                case CollisionBoundryDirection.Down:
                    // Return the highest point on the segmented boundry (farthest from the top of the screen) and additional buffer so
                    // that collisions can be detected for an extra number pixels under the highest point.
                    bottomValue = highestPointYValue + boundryBufferZone;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new CollisionBoundingRect 
            {
                Left = leftmostPointXValue,
                Right = rightmostPointXValue,
                Top = lowestPointYValue,
                Bottom = bottomValue,
            };
        }
    }
}
