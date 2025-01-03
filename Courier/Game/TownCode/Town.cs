﻿using Courier.Engine.Collisions;
using Courier.Engine.Collisions.CollisionShapes;
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
        private const CollisionNodeType collisionType = CollisionNodeType.Town;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.Parcel };
        private const float CollisionWidth = 75;
        private const float CollisionHeight = 12;

        /// <summary>
        /// Boolean value representing if the town has been destroyed.
        /// </summary>
        public bool Destroyed { get; private set; }

        public Town(Node parent) : base(parent)
        {
            spriteOffset = new Vector2(0, -23);
            sprite = new Sprite(this, "Town")
            {
                LocalPosition = spriteOffset,
            };
            Children.Add(sprite);

            var collisionShape = new CollisionRectangle(CollisionWidth, CollisionHeight);
            collisionNode = new CollisionNode(this, collisionShape, collisionType, collisionTypeMask);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);
        }

        private void TownHitByParcel()
        {
            if (Destroyed)
            {
                return;
            }

            // Update the town sprite
            Children.Remove(sprite);
            sprite = new Sprite(this, "TownCelebrate")
            {
                LocalPosition = spriteOffset,
            };
            Children.Add(sprite);

            Destroyed = true;
        }

        private void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            TownHitByParcel();
        }
    }
}
