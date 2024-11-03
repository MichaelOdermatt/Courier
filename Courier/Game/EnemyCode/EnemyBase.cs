using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public abstract class EnemyBase : Node
    {
        protected readonly Player player;
        protected readonly Sprite sprite;

        protected readonly BulletPool bulletPool;
        protected readonly GameTimer shootTimer;
        protected readonly float timeBetweenBullets;
        protected readonly float shootRange;

        private Action CreateBulletAction;

        /// <param name="timeBetweenBullets">The time interval between bullets fired by the enemy.</param>
        /// <param name="shootRange">The range that the player must be in for the enemy to shoot at them.</param>
        /// <param name="textureKey">The texture key for the emeny's sprite.</param>
        public EnemyBase(
            Node parent, 
            Player player, 
            BulletPool bulletPool, 
            float timeBetweenBullets, 
            float shootRange,
            string textureKey
        ) : base(parent)
        {
            this.timeBetweenBullets = timeBetweenBullets;
            this.shootRange = shootRange;
            this.player = player;
            this.bulletPool = bulletPool;
            CreateBulletAction = TryCreateBullet;

            sprite = new Sprite(this, textureKey);
            Children.Add(sprite);

            // Create the shoot timer.
            shootTimer = new GameTimer(timeBetweenBullets, CreateBulletAction);
            shootTimer.Loop = true;
            shootTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Tick the shootTimer.
            shootTimer.Tick(gameTime);
        }


        /// <summary>
        /// Attempts to create a Bullet and fires it in the direction of the player.
        /// </summary>
        public abstract void TryCreateBullet();
    }
}
