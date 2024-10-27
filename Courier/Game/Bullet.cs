using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{

    public class Bullet : Node, ICollisionNode
    {
        private readonly Sprite sprite;

        private Vector2 bulletDirection;

        private readonly float speed = 250f;

        /// <summary>
        /// Boolean representing if the Bullet is active. i.e. is visible, moving, and able to collide with the player.
        /// </summary>
        public bool IsActive { get; set; } = false;
        public ICollisionShape CollisionShape { get; set; }
        public bool CollisionsEnabled { get; set; } = false;

        public Bullet(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "BulletSmall");
            Children.Add(sprite);

            CollisionShape = new CollisionSphere(2f);
            CollisionShape.Parent = this;

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
            CollisionsEnabled = true;
            sprite.Visible = true;
        }

        /// <summary>
        /// Hides the bullet and stops it from moving or colliding with the player.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            CollisionsEnabled = false;
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
    }
}
