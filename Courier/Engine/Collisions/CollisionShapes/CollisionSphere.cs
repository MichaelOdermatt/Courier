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
        public float Radius { get; private set; }

        public CollisionSphere(float radius)
        {
            Radius = radius;
        }

        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix)
        {
            var transformedPoint = Vector2.Transform(Vector2.Zero, transformMatrix);
            return new CollisionBoundingRect {
                Left = transformedPoint.X - Radius,
                Right = transformedPoint.X + Radius,
                Top = transformedPoint.Y - Radius,
                Bottom = transformedPoint.X + Radius,
            };
        }
    }
}
