using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.TownCode
{
    public class Town : Node
    {
        public Sprite sprite;
        public readonly Vector2 spriteOffset;

        private readonly CollisionNode collisionNode;
        private const float CollisionRadius = 10;

        private bool destroyed;

        public Town(Node parent) : base(parent)
        {
            spriteOffset = new Vector2(0, -23);
            sprite = new Sprite(this, "Town")
            {
                Offset = spriteOffset,
            };
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, CollisionRadius);
            collisionNode = new CollisionNode(this, collisionShape, CollisionNodeType.Town);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);
        }

        private void TownHitByParcel()
        {
            if (destroyed)
            {
                return;
            }

            // Update the town sprite
            Children.Remove(sprite);
            sprite = new Sprite(this, "TownCelebrate")
            {
                Offset = spriteOffset,
            };
            Children.Add(sprite);

            destroyed = true;
        }

        private void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            if (eventArgs.collisionNode.CollisionNodeType == CollisionNodeType.Parcel)
            {
                TownHitByParcel();
            }
        }
    }
}
