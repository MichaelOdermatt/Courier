using Courier.Engine.Collisions;
using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class RefuelPickup : Node
    {
        private readonly Sprite sprite;

        private readonly CollisionNode collisionNode;
        private const CollisionNodeType collisionType = CollisionNodeType.RefuelPickup;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.Player };

        public RefuelPickup(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "OilDrum");
            Children.Add(sprite);

            var collisionShape = new CollisionRectangle(this, 15, 20);
            collisionNode = new CollisionNode(this, collisionShape, collisionType, collisionTypeMask);
            collisionNode.OnCollision += OnRefuelPickupCollision;
            Children.Add(collisionNode);
        }

        private void OnRefuelPickupCollision(object sender, CollisionEventArgs eventArgs)
        {
            // Disable the collisionNode after the first collision, that way no more collisions will take place with this node.
            collisionNode.CollisionsEnabled = false;
            sprite.Visible = false;
        }
    }
}
