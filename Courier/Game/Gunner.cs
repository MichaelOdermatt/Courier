using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
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
        private const float TimeBetweenBullets = 0.75f;
        private const float ShootRange = 500f;
        private const float EnemyTargetThresholdAngle = 1.57f;

        private Action CreateBulletAction;

        public Gunner(Node parent, Player player, BulletPool bulletPool) : base(parent)
        {
            this.player = player;
            this.bulletPool = bulletPool;
            CreateBulletAction = TryCreateBullet;

            sprite = new Sprite(this, "Gunner");
            Children.Add(sprite);

            // Create the shoot timer.
            shootTimer = new GameTimer(TimeBetweenBullets, CreateBulletAction);
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
        public void TryCreateBullet()
        {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            // Don't shoot if the player is out of range.
            if (vectorToPlayer.Length() > ShootRange)
            {
                return;
            }

            var pointToShootAt = PointToShootAt();

            // Rotate the sprite to face the Player. The sprite rotation will be updated in the next Draw call.
            var angleToPoint = MathF.Atan2(-pointToShootAt.Y, -pointToShootAt.X);
            sprite.Rotation = angleToPoint;

            pointToShootAt.Normalize();
            bulletPool.ActivateBullet(GlobalPosition, pointToShootAt, BulletType.Small);
        }

        /// <summary>
        /// Returns the point that the Gunner should shoot at. It is either the Player itself or the Player's EnemyTarget
        /// </summary>
        private Vector2 PointToShootAt()
        {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            var vectorToEnemyTarget = player.EnemyTargetGlobalPosition - GlobalPosition;

            var dotProduct = Vector2.Dot(vectorToPlayer, vectorToEnemyTarget);
            var angle = MathF.Acos(dotProduct / (vectorToPlayer.Length() * vectorToEnemyTarget.Length()));

            // We calculate the angle between the Player and the Player's EnemyTarget relative to the Gunner. If the angle is sufficiently large
            // it means the EnemyTarget and Player are on either side of the Gunner, so point at the Player.
            return angle >= EnemyTargetThresholdAngle ? vectorToPlayer : vectorToEnemyTarget;
        }
    }
}
