using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public class CollisionRectangle : ICollisionShape
    {
        public float Width { get; private set; }

        public float Height { get; private set; }

        public CollisionRectangle(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix)
        {
            // Transform all the corner points of the rectangle and add them to a list.
            var points = new List<Vector2>() {
                Vector2.Transform(new Vector2(-Height * 0.5f, Width * 0.5f), transformMatrix),
                Vector2.Transform(new Vector2(Height * 0.5f, Width * 0.5f), transformMatrix),
                Vector2.Transform(new Vector2(-Height * 0.5f, -Width * 0.5f), transformMatrix),
                Vector2.Transform(new Vector2(Height * 0.5f, -Width * 0.5f), transformMatrix),
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
