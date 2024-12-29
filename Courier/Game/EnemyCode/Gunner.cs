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
using System.Threading;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public class Gunner : EnemyBase
    {
        private const float BulletSpeed = 275f;
        private const float MaxPlayerPositionPrediction = 175f;

        /// <summary>
        /// The amount in radians that the Gunner's accuracy can randomly deviate.
        /// </summary>
        private float accuracyDeviation = 0.05f;
        private readonly Random random;

        public Gunner(Node parent, Player player) : base(parent, player, 0.30f, 500f, "Gunner", 5f)
        {
            random = new Random();
        }

        protected override void UpdateShootTimerDuration()
        {
            float newShootTimerDuration;
            switch(state)
            {
                case EnemyState.OneStar:
                    newShootTimerDuration = 0.30f;
                    break;
                case EnemyState.ThreeStar:
                    newShootTimerDuration = 0.25f;
                    accuracyDeviation = 0.03f;
                    break;
                case EnemyState.FiveStar:
                    newShootTimerDuration = 0.2f;
                    accuracyDeviation = 0.01f;
                    break;
                default:
                    newShootTimerDuration = shootTimer.Duration;
                    break;
            }
            shootTimer.Duration = newShootTimerDuration;
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

            var pointToShootAt = PredictPlayerPosition();

            // Rotate the sprite to face the Player. The sprite rotation will be updated in the next Draw call.
            var angleToPoint = MathF.Atan2(-pointToShootAt.Y, -pointToShootAt.X);
            sprite.Rotation = angleToPoint;

            // From the PointToShootAt Get the direction that the enemy should fire the bullet.
            var shootDirection = Vector2.Normalize(pointToShootAt - GlobalPosition);
            var shootDirectionDeviation = -accuracyDeviation + (float)random.NextDouble() * 2.0f * accuracyDeviation;
            shootDirection = shootDirection.Rotate(shootDirectionDeviation);
            // Create the bullet
            hub.Publish(new FireBulletEvent
            {
                InitialPosition = GlobalPosition,
                Direction = shootDirection,
                BulletType = BulletType.Small,
                BulletSpeed = BulletSpeed
            });
        }

        private Vector2 PredictPlayerPosition()
        {
            var bulletSpeed = BulletSpeed;
            var playerVelocity = player.Velocity;

            Vector2 predictedPlayerPosition = player.GlobalPosition;
            var vectorToPredictedPosition = player.GlobalPosition - GlobalPosition;
            for (var i = 0; i <= 4; i++)
            {
                // Find the time it will take for the bullet to reach the predicted position.
                var timeToHit = vectorToPredictedPosition.Length() / bulletSpeed;
                // Update the predicted position.
                predictedPlayerPosition = player.GlobalPosition + playerVelocity * timeToHit;
                vectorToPredictedPosition = predictedPlayerPosition - GlobalPosition;
            }

            // If the predicted position is farther than the max, return the max.
            if (predictedPlayerPosition.Length() > MaxPlayerPositionPrediction)
            {
                return player.GlobalPosition + Vector2.Normalize(playerVelocity) * MaxPlayerPositionPrediction;
            }

            return predictedPlayerPosition;
        }
    }
}
