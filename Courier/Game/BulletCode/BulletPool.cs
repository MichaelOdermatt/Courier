using Courier.Engine;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public enum BulletType
    {
        Small,
        Large,
    }

    public class BulletPool : Node
    {
        private readonly GameTimer[] smallBulletTimers;
        private readonly GameTimer[] largeBulletTimers;
        private readonly Bullet[] smallBullets;
        private readonly Bullet[] largeBullets;

        public const float bulletActiveDuration = 3f;
        public int numOfBullets;

        public const string smallBulletTextureKey = "BulletSmall";
        public const float smallBulletRadius = 2f;

        public const string largeBulletTextureKey = "BulletLarge";
        public const float largeBulletRadius = 5f;

        public BulletPool(Node parent, int numOfBullets) : base(parent)
        {
            this.numOfBullets = numOfBullets;
            // Create the array of Bullets and timers. Their indexes line up, so the Bullet at index i will have its timer at index i.
            smallBulletTimers = new GameTimer[numOfBullets];
            largeBulletTimers = new GameTimer[numOfBullets];
            smallBullets = new Bullet[numOfBullets];
            largeBullets = new Bullet[numOfBullets];

            for (int i = 0; i < numOfBullets; i++)
            {
                var newSmallBullet = new Bullet(this, smallBulletTextureKey, smallBulletRadius);
                // Add the Bullets as children so their Draw and Update functions can be called.
                Children.Add(newSmallBullet);

                smallBullets[i] = newSmallBullet;
                smallBulletTimers[i] = new GameTimer(bulletActiveDuration, () => newSmallBullet.Deactivate());

                var newLargeBullet = new Bullet(this, largeBulletTextureKey, largeBulletRadius);
                // Add the Bullets as children so their Draw and Update functions can be called.
                Children.Add(newLargeBullet);

                largeBullets[i] = newLargeBullet;
                largeBulletTimers[i] = new GameTimer(bulletActiveDuration, () => newLargeBullet.Deactivate());
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < numOfBullets; i++)
            {
                var smallBulletTimer = smallBulletTimers[i];
                smallBulletTimer.Tick(gameTime);

                var largeBulletTimer = largeBulletTimers[i];
                largeBulletTimer.Tick(gameTime);
            }
        }

        /// <summary>
        /// Activates, or creates, a Bullet from the pool. The Bullet will be deactivated (or removed) after the set amount of time specified in the BulletPool.
        /// </summary>
        /// <param name="initialPosition">The initial position of the Bullet.</param>
        /// <param name="direction">The direction the Bullet should fly.</param>
        /// <param name="bulletType">The type the Bullet to activate.</param>
        public void ActivateBullet(Vector2 initialPosition, Vector2 direction, BulletType bulletType)
        {
            var inactiveBulletIndex = TryGetInactiveBulletIndex(bulletType);
            if (inactiveBulletIndex == -1)
            {
                throw new InvalidOperationException("Could not get Inactive Bullet from the BulletPool. Try increasing the number of Bullets in the pool.");
            }

            var inactiveBullet = bulletType == BulletType.Small ? smallBullets[inactiveBulletIndex] : largeBullets[inactiveBulletIndex];
            var inactiveBulletTimer = bulletType == BulletType.Small ? smallBulletTimers[inactiveBulletIndex] : largeBulletTimers[inactiveBulletIndex];

            inactiveBulletTimer.Start();
            inactiveBullet.Activate(initialPosition, direction);
        }

        /// <summary>
        /// Returns the first inactive Bullet from the list of Bullets. Returns -1 if no inactive bullet was found.
        /// </summary>
        /// <param name="bulletType">The type the Bullet to the index of.</param>
        private int TryGetInactiveBulletIndex(BulletType bulletType)
        {
            for (int i = 0; i < numOfBullets; i++)
            {
                var bullet = bulletType == BulletType.Small ? smallBullets[i] : largeBullets[i];
                if (!bullet.IsActive)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
