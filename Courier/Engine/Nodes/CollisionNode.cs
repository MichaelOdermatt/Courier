using Auios.QuadTree;
using Courier.Engine.Collisions;
using Courier.Engine.Collisions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Nodes
{
    public class CollisionNode : Node, ICollisionNode
    {
        public ICollisionShape CollisionShape { get; private set; }
        public CollisionNodeType CollisionNodeType { get; private set; }
        public CollisionNodeType[] CollisionNodeTypeMask { get; private set; }
        public bool CollisionsEnabled { get; set; } = true;

        public CollisionNode(Node parent, ICollisionShape collisionShape, CollisionNodeType collisionNodeType, CollisionNodeType[] collisionNodeTypeMask) : base(parent)
        {
            CollisionShape = collisionShape;
            CollisionNodeType = collisionNodeType;
            CollisionNodeTypeMask = collisionNodeTypeMask;
        }

        public event EventHandler<CollisionEventArgs> OnCollision;

        public bool IsMaskingNodeType(CollisionNodeType collisionNodeType)
        {
            if (CollisionNodeTypeMask == null)
            {
                return false;
            }

            for (int i = 0; i < CollisionNodeTypeMask.Length; i++)
            {
                if (CollisionNodeTypeMask[i] == collisionNodeType)
                {
                    return true;
                }
            }

            return false;
        }

        public void Collide(ICollisionNode collidedNode)
        {
            if (OnCollision != null)
            {
                OnCollision(this, new CollisionEventArgs
                {
                    collisionNode = collidedNode,
                });
            }
        }

        public float GetBottom() => GlobalPosition.Y + CollisionShape.GetBottom();

        public float GetTop() => GlobalPosition.Y + CollisionShape.GetTop();

        public float GetLeft() => GlobalPosition.X + CollisionShape.GetLeft();

        public float GetRight() => GlobalPosition.X + CollisionShape.GetRight();

        /// <summary>
        /// Returns the CollisionNode as a QuadTreeRect. The QuadTreeRect will represent the CollisionNode's width, height, and position in world space.
        /// </summary>
        public QuadTreeRect GetQuadTreeRect() 
        {
            var height = GetBottom() - GetTop();
            var width = GetRight() - GetLeft();
            var x = GlobalPosition.X;
            var y = GlobalPosition.Y;
            // X and Y represent the top left of the rect.
            return new QuadTreeRect(x, y, width, height);
        }
    }
}
