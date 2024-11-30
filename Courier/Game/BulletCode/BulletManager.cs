using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public class BulletManager : Node
    {
        private readonly Hub hub;

        private readonly List<BulletBase> bullets = new List<BulletBase>();
        private readonly List<GameTimer> bulletTimers = new List<GameTimer>();

        private const float BulletActiveTime = 5f;

        public BulletManager(Node parent) : base(parent)
        {
            hub = Hub.Default;
            hub.Subscribe<FireBulletEvent>(this, OnFireBullet);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Remove any parcels that have been marked as Destroyed
            var bulletsToRemove = bullets.Where(parcel => parcel.Destroyed);
            Children.RemoveAll(c => bulletsToRemove.Contains(c));
            bullets.RemoveAll(p => bulletsToRemove.Contains(p));

            bulletTimers.ForEach(timer => timer.Tick(gameTime));
            // Remove any bullet timers from the list that are no longer running.
            bulletTimers.RemoveAll(p => !p.IsRunning);
        }

        /// <summary>
        /// Creates a new bullet at the position in the given event data.
        /// Also creates a game timer to destroy the bullet after a set amount of time.
        /// </summary>
        private void OnFireBullet(FireBulletEvent eventData)
        {
            BulletBase newBullet;
            if (eventData.BulletType == BulletType.Small)
            {
                newBullet = new SmallBullet(this, eventData.InitialPosition, eventData.Direction);
            } else
            {
                newBullet = new LargeBullet(this, eventData.InitialPosition, eventData.Direction);
            }
            Children.Add(newBullet);
            bullets.Add(newBullet);

            var newTimer = new GameTimer(BulletActiveTime, () => DestroyBullet(newBullet));
            newTimer.Start();
            bulletTimers.Add(newTimer);
        }

        private void DestroyBullet(BulletBase bullet)
        {
            Children.Remove(bullet);
            bullets.Remove(bullet);
        }
    }
}
