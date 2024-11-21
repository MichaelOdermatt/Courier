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
        public bool CollisionsEnabled { get; set; } = true;

        public CollisionNode(Node parent, ICollisionShape collisionShape, CollisionNodeType collisionNodeType) : base(parent)
        {
            CollisionShape = collisionShape;
            CollisionNodeType = collisionNodeType;
        }

        public event EventHandler<CollisionEventArgs> OnCollision;

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
    }
}
