using System;
using System.Collections.Generic;
using System.Linq;
using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public class CollisionRaycast : ICollisionShape
    {
        public float Length { get; private set; }

        public CollisionRaycast(float length) {
            Length = length;
        }

        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix)
        {
            // Transform both ends of the raycast and add them to a list.
            var points = new List<Vector2>() {
                Vector2.Transform(Vector2.Zero, transformMatrix),
                Vector2.Transform(new Vector2(Length, 0), transformMatrix),
            };

            return new CollisionBoundingRect 
            {
                Left = points.Min(p => p.X),
                Right = points.Max(p => p.X),
                Top = points.Min(p => p.Y),
                Bottom = points.Max(p => p.Y),
            };
        }
    }
}