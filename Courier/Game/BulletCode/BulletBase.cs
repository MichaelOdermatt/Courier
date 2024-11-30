using Courier.Engine.Collisions;
using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public abstract class BulletBase : Node
    {
        private readonly Sprite sprite;
        private readonly CollisionNode collisionNode;

        public Vector2 bulletDirection;
        private readonly float speed;

        /// <summary>
        /// Bool value used to mark whether this bullet has been destroyed or not.
        /// </summary>
        public bool Destroyed { get; set; } = false;

        public BulletBase(
            Node parent, 
            string textureKey, 
            float bulletRadius, 
            float bulletSpeed, 
            CollisionNodeType collisionNodeType,
            Vector2 initialPos,
            Vector2 direction) : base(parent)
        {
            LocalPosition = initialPos;
            bulletDirection = direction;
            speed = bulletSpeed;
            sprite = new Sprite(this, textureKey);
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, bulletRadius);
            collisionNode = new CollisionNode(this, collisionShape, collisionNodeType);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var velocity = bulletDirection * (speed * deltaTime);

            // Apply the velocity to the Bullet.
            LocalPosition += velocity;
        }

        public void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            switch (eventArgs.collisionNode.CollisionNodeType)
            {
                case CollisionNodeType.Enemy:
                case CollisionNodeType.Town:
                    return;
            }
            Destroyed = true;
        }
    }
}
