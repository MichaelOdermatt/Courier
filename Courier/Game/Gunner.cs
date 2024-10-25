using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Gunner : Node
    {
        private readonly Player player;
        private readonly Sprite sprite;

        private readonly BulletPool bulletPool;
        private readonly GameTimer shootTimer;
        private readonly float timeBetweenBullets = 0.75f;
        private readonly float shootRange = 500f;

        private Action CreateBulletAction;

        public Gunner(Node parent, Player player, BulletPool bulletPool) : base(parent)
        {
            this.player = player;
            this.bulletPool = bulletPool;
            CreateBulletAction = TryCreateBullet;

            sprite = new Sprite(this, "Gunner");
            Children.Add(sprite);

            // Create the shoot timer.
            shootTimer = new GameTimer(timeBetweenBullets, CreateBulletAction);
            shootTimer.Loop = true;
            shootTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Rotate the sprite to face the Player.
            var vectorToPlayer = GlobalPosition - player.EnemyTargetGlobalPosition;
            var angleToPlayer = MathF.Atan2(vectorToPlayer.Y, vectorToPlayer.X);
            sprite.Rotation = angleToPlayer;

            // Tick the shootTimer.
            shootTimer.Tick(gameTime);
        }

        /// <summary>
        /// Attempts to create a Bullet and fires it in the direction of the player.
        /// </summary>
        public void TryCreateBullet()
        {
            var vectorToPlayer = player.EnemyTargetGlobalPosition - GlobalPosition;
            // Don't shoot if the player is out of range.
            if (vectorToPlayer.Length() > shootRange)
            {
                return;
            }

            vectorToPlayer.Normalize();
            bulletPool.ActivateBullet(GlobalPosition, vectorToPlayer);
        }
    }
}
