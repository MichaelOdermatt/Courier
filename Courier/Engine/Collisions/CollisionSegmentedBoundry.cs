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
    /// <summary>
    /// A CollisionShape representing a level boundry that consists of a segmented line rather than a straight line. 
    /// </summary>
    public class CollisionSegmentedBoundry : ICollisionShape
    {
        /// <summary>
        /// The points that make up the segmented line.
        /// </summary>
        public readonly Vector2[] Points;

        /// <summary>
        /// The direction in which the collision shape should bound off.
        /// </summary>
        public readonly SegmentedBoundryDirection Direction;

        /// <summary>
        /// A Comparer object for Vector2 that is used with binary search to detect which Vector2 points the player is closest to.
        /// </summary>
        public static Comparer<Vector2> Vector2Comparer { get; } = Comparer<Vector2>.Create((a, b) => a.X > b.X ? 1 : a.X < b.X ? -1 : 1);

        public CollisionSegmentedBoundry(Vector2[] points, SegmentedBoundryDirection direction)
        {
            Points = points;
            Direction = direction;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSphere collisionSphere)
            {
                return collisionShape.Intersects(this);
            } else
            {
                // Throwing a NotImplementedException since we don't need to wory about calling intersects on a CollisionPolygon at the moment.
                throw new NotImplementedException();
            }
        }
    }
}
