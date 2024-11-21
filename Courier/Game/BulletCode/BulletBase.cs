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

        private Vector2 bulletDirection;

        private readonly float speed;

        /// <summary>
        /// Boolean representing if the Bullet is active. i.e. is visible, moving, and able to collide with the player.
        /// </summary>
        public bool IsActive { get; set; } = false;

        public BulletBase(Node parent, string textureKey, float bulletRadius, float bulletSpeed, CollisionNodeType collisionNodeType) : base(parent)
        {
            speed = bulletSpeed;
            sprite = new Sprite(this, textureKey);
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, bulletRadius);
            collisionNode = new CollisionNode(this, collisionShape, collisionNodeType);
            collisionNode.CollisionsEnabled = false;
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);

            // The bullet starts as inactive, so hide the sprite.
            sprite.Visible = false;
        }

        /// <summary>
        /// Sets the bullet and visible, moving, and able to collide with the player.
        /// </summary>
        /// <param name="initialPosition">The initial position of the Bullet.</param>
        /// <param name="direction">The direction the Bullet should fly.</param>
        public void Activate(Vector2 initialPosition, Vector2 direction)
        {
            LocalPosition = initialPosition;
            bulletDirection = direction;
            IsActive = true;
            collisionNode.CollisionsEnabled = true;
            sprite.Visible = true;
        }

        /// <summary>
        /// Hides the bullet and stops it from moving or colliding with the player.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            collisionNode.CollisionsEnabled = false;
            sprite.Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsActive)
            {
                return;
            }

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var velocity = bulletDirection * (speed * deltaTime);

            // Apply the velocity to the Bullet.
            LocalPosition += velocity;
        }

        public void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            Deactivate();
        }
    }
}
