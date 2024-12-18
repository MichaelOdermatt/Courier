using Courier.Engine;
using Courier.Engine.Extensions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.EventData;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public class Tank : EnemyBase
    {
        private const float BulletSpeed = 150f;

        /// <summary>
        /// The amount in radians that the Gunner's accuracy can randomly deviate.
        /// </summary>
        private float accuracyDeviation = 0.05f;
        private readonly Random random;

        public Tank(Node parent, Player player) : base(parent, player, 0.95f, 700f, "Tank", 10f)
        {
            random = new Random();
        }

        protected override void UpdateShootTimerDuration()
        {
            float newShootTimerDuration;
            switch(state)
            {
                case EnemyState.OneStar:
                    newShootTimerDuration = 0.95f;
                    break;
                case EnemyState.ThreeStar:
                    newShootTimerDuration = 0.7f;
                    break;
                case EnemyState.FiveStar:
                    newShootTimerDuration = 0.5f;
                    break;
                default:
                    newShootTimerDuration = shootTimer.Duration;
                    break;
            }
            shootTimer.Duration = newShootTimerDuration;
        }

        /// <summary>
        /// Attempts to create a Bullet and fires it directly up.
        /// </summary>
        protected override void TryCreateBullet()
        {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            // Don't shoot if the player is out of range.
            if (vectorToPlayer.Length() > shootRange)
            {
                return;
            }

            var shootDirectionDeviation = -accuracyDeviation + ((float)random.NextDouble() * 2.0f) * accuracyDeviation;
            var shootDirection = -Vector2.UnitY.Rotate(shootDirectionDeviation);
            hub.Publish(new FireBulletEvent
            {
                InitialPosition = GlobalPosition,
                Direction = shootDirection,
                BulletType = BulletType.Large,
                BulletSpeed = BulletSpeed
            });
        }
    }
}
