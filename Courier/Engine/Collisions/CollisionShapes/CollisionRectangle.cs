using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public class CollisionRectangle : ICollisionShape
    {
        public float Width { get; private set; }

        public float Height { get; private set; }

        /// <summary>
        /// The Node which the CollisionSphere is attached to.
        /// </summary>
        private readonly Node instantiatingNode;

        public Vector2 GlobalPosition { get => instantiatingNode.GlobalPosition; }

        public CollisionRectangle(Node node, float width, float height)
        {
            instantiatingNode = node;
            Width = width;
            Height = height;
        }

        public bool Intersects(ICollisionShape collisionShape)
        {
            if (collisionShape is CollisionSphere collisionSphere)
            {
                return CheckIntersections.SphereIntersectsWithRectangle(collisionSphere, this);
            }

            throw new NotImplementedException();
        }

        public float GetBottom() => Height * 0.5f;

        public float GetTop() => -Height * 0.5f;

        public float GetLeft() => -Width * 0.5f;

        public float GetRight() => Width * 0.5f;

    }
}
