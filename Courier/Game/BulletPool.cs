using Courier.Engine;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class BulletPool : Node
    {
        private readonly GameTimer[] bulletTimers;
        private readonly Bullet[] bullets;

        public float bulletActiveDuration = 3f;
        public int numOfBullets;

        public BulletPool(Node parent, int numOfBullets) : base(parent)
        {
            this.numOfBullets = numOfBullets;
            // Create the array of Bullets and timers. Their indexes line up, so the Bullet at index i will have its timer at index i.
            bulletTimers = new GameTimer[numOfBullets];
            bullets = new Bullet[numOfBullets];

            for (int i = 0; i < numOfBullets; i++)
            {
                var newBullet = new Bullet(this);
                // Add the Bullets as children so their Draw and Update functions can be called.
                Children.Add(newBullet);

                bullets[i] = newBullet;
                bulletTimers[i] = new GameTimer(bulletActiveDuration, () => newBullet.Deactivate());
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < this.numOfBullets; i++)
            {
                var bulletTimer = bulletTimers[i];
                bulletTimer.Tick(gameTime);
            }
        }

        /// <summary>
        /// Activates, or creates, a Bullet from the pool. The Bullet will be deactivated (or removed) after the set amount of time specified in the BulletPool.
        /// </summary>
        /// <param name="initialPosition">The initial position of the Bullet.</param>
        /// <param name="direction">The direction the Bullet should fly.</param>
        public void ActivateBullet(Vector2 initialPosition, Vector2 direction)
        {
            var inactiveBulletIndex = TryGetInactiveBulletIndex();
            if (inactiveBulletIndex == -1)
            {
                throw new InvalidOperationException("Could not get Inactive Bullet from the BulletPool. Try increasing the number of Bullets in the pool.");
            }

            var inactiveBullet = bullets[inactiveBulletIndex];
            var inactiveBulletTimer = bulletTimers[inactiveBulletIndex];

            inactiveBulletTimer.Start();
            inactiveBullet.Activate(initialPosition, direction);
        }

        /// <summary>
        /// Returns the first inactive Bullet from the list of Bullets. Returns -1 if no inactive bullet was found.
        /// </summary>
        private int TryGetInactiveBulletIndex()
        {
            for (int i = 0; i < this.numOfBullets; i++)
            {
                var bullet = bullets[i];
                if (!bullet.IsActive)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
