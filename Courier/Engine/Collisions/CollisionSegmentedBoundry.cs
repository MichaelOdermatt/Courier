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
        public Node Parent { get; set; }

        /// <summary>
        /// The points that make up the segmented line.
        /// </summary>
        public readonly Vector2[] Points;

        /// <summary>
        /// The direction in which the collision shape should bound off.
        /// </summary>
        public readonly SegmentedBoundryDirection Direction;

        public CollisionSegmentedBoundry(Vector2[] points, SegmentedBoundryDirection direction)
        {
            Points = points;
            Direction = direction;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            // Throwing a NotImplementedException since we don't need to wory about calling intersects on a CollisionPolygon at the moment.
            throw new NotImplementedException();
        }
    }
}
