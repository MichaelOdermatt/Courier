using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.EventData;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public class Gunner : EnemyBase
    {
        private const float EnemyTargetThresholdAngle = 1.57f;

        public Gunner(Node parent, Player player) : base(parent, player, 0.75f, 500f, "Gunner", 5f)
        {
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
        protected override void TryCreateBullet()
        {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            // Don't shoot if the player is out of range.
            if (vectorToPlayer.Length() > shootRange)
            {
                return;
            }

            var pointToShootAt = PointToShootAt();

            // Rotate the sprite to face the Player. The sprite rotation will be updated in the next Draw call.
            var angleToPoint = MathF.Atan2(-pointToShootAt.Y, -pointToShootAt.X);
            sprite.Rotation = angleToPoint;

            pointToShootAt.Normalize();
            hub.Publish(new FireBulletEvent
            {
                InitialPosition = GlobalPosition,
                Direction = pointToShootAt,
                BulletType = BulletType.Small,
            });
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
