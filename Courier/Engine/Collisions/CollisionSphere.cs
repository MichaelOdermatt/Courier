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
        private Vector2 position;
        private float radius;

        public CollisionSphere(float radius)
        {
            this.radius = radius;
        }

        /// <inheritdoc/>
        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSegmentedBoundry collisionSB && collisionSB.Direction == SegmentedBoundryDirection.Down)
            {
                // TODO could optimize this by storing the points differently?

                Vector2? leftPoint = null;
                Vector2? rightPoint = null;
                for (int i = 0; i < collisionSB.Points.Length - 1; i++)
                {
                    var currentPoint = collisionSB.Points[i];
                    var nextPoint = collisionSB.Points[i + 1];
                    if (currentPoint.X <= position.X && nextPoint.X >= position.X)
                    {
                        leftPoint = currentPoint;
                        rightPoint = nextPoint;
                        break;
                    }
                }

                if (leftPoint == null || rightPoint == null)
                {
                    return false;
                }

                // TODO put this in it's own math function?
                var slope = (rightPoint.Value.Y - leftPoint.Value.Y) / (rightPoint.Value.X - leftPoint.Value.X);
                var yAtXPos = slope * (position.X - leftPoint.Value.X) + leftPoint.Value.Y;

                if (yAtXPos >= position.X)
                {
                    return true;
                }

                return false;
            }

            throw new NotImplementedException();
        }
    }
}
