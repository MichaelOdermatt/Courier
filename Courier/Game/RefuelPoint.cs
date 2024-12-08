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
    public class RefuelPoint : Node
    {
        private readonly Sprite sprite;
        private const float LineThickeness = 1f;
        private const float LineLength = 1000f;

        private readonly CollisionNode collisionNode;
        private const CollisionNodeType collisionType = CollisionNodeType.RefuelPoint;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.Player };

        public RefuelPoint(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "LineTextureWhite");
            sprite.Scale = new Vector2(LineLength, LineThickeness);
            sprite.Rotation = -MathF.PI * 0.5f;
            sprite.NeverCull = true;
            Children.Add(sprite);

            var collisionShape = new CollisionLineBoundry(this, CollisionBoundryDirection.Right);
            collisionNode = new CollisionNode(this, collisionShape, collisionType, collisionTypeMask);
            collisionNode.OnCollision += OnRefuelPointCollision;
            Children.Add(collisionNode);
        }

        private void OnRefuelPointCollision(object sender, CollisionEventArgs eventArgs)
        {
            // Disable the collisionNode after the first collision, that way no more collisions will take place with this node.
            collisionNode.CollisionsEnabled = false;
            sprite.Visible = false;
        }
    }
}
