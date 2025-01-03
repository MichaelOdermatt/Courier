using Auios.QuadTree;
using Courier.Engine.Collisions;
using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Collisions.Interfaces;
using Microsoft.Xna.Framework;
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

        public QuadTreeRect GetQuadTreeRect() 
        {
            var boundingRect = GetCollisionBoundingRect();
            var height = boundingRect.Bottom - boundingRect.Top;
            var width = boundingRect.Right - boundingRect.Left;
            var x = GlobalPosition.X;
            var y = GlobalPosition.Y;
            // X and Y represent the top left of the rect.
            return new QuadTreeRect(x, y, width, height);
        }

        public CollisionBoundingRect GetCollisionBoundingRect()
        {
            var translationTransform = Matrix.CreateTranslation(GlobalPosition.X, GlobalPosition.Y, 0);
            var rotationTransform = Matrix.CreateRotationZ(GlobalRotation);
            var matrixTransform = translationTransform * rotationTransform;

            // Apply the rotation and position to the shape as a transform matrix then return the resulting bounding rectangle.
            return CollisionShape.GetBoundingRect(matrixTransform);
        }
    }
}
