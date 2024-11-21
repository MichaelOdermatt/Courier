using Courier.Engine.Collisions;
using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.ParcelCode
{
    public class Parcel : Node
    {
        private readonly Sprite sprite;
        private readonly CollisionNode collisionNode;
        private readonly Vector2 parcelDirection = Vector2.UnitY;
        private Vector2 velocity = Vector2.Zero;

        private const float Speed = 4;
        private const float collisionRadius = 5f;

        public Parcel(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "Parcel");
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, collisionRadius);
            collisionNode = new CollisionNode(this, collisionShape, CollisionNodeType.Parcel);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += parcelDirection * (Speed * deltaTime);

            // Apply the velocity to the Parcel.
            LocalPosition += velocity;
        }

        public void OnCollision(object sender, CollisionEventArgs collisionNode)
        {
        }
    }
}
