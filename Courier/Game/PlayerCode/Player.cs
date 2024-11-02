using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Extensions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class Player : PlayerController
    {
        private readonly Sprite sprite;
        /// <summary>
        /// A Node which has the Players position with an additional offset. This is what Enemies should fire at.
        /// </summary>
        private readonly Node enemyTarget;
        private readonly Camera2D camera;
        private readonly FuelMeterElement fuelMeterElement;
        private readonly PlayerMovement playerMovement = new PlayerMovement();
        private readonly PlayerHealth playerHealth = new PlayerHealth();

        private PlayerState playerState = PlayerState.Alive;

        private const float MaxEnemyTargetDistance = 500f;
        private const float SpeedAtMaxEnemyTargetDistance = 12f;

        /// <summary>
        /// The GlobalPosition of the EnemyTarget object.
        /// </summary>
        public Vector2 EnemyTargetGlobalPosition { get => enemyTarget.GlobalPosition; }

        public Player(Node parent, Camera2D camera, FuelMeterElement fuelMeterElement) : base(parent)
        {
            sprite = new Sprite(this, "Player");
            Children.Add(sprite);

            enemyTarget = new Node(this);
            Children.Add(enemyTarget);

            CollisionShape = new CollisionSphere(this, 25);
            
            this.camera = camera;
            this.fuelMeterElement = fuelMeterElement;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (playerState == PlayerState.Destroyed)
            {
                return; 
            }

            Velocity = playerMovement.CalcNewVelocity(gameTime, Velocity);
            fuelMeterElement.UpdateMeterFill(playerMovement.FuelAmountScaled);
            ApplyVelocity();

            // TODO lerp the rotation, so it looks smoother? Or would that make it look strange
            // Update the sprites rotation to match the angle of attack.
            var angleOfAttack = playerMovement.CalcAngleOfAttack(Velocity);
            sprite.Rotation = angleOfAttack;

            UpdateEnemyTargetPosition();

            // Update the camera position to always follow the Player.
            camera.SetPosition(GlobalPosition);
        }

        public override void OnCollision(ICollisionNode collisionNode)
        {
            if (collisionNode is Bullet bulletNode)
            {
                playerHealth.reduceHealth(1);
                bulletNode.Deactivate();
            } else if (collisionNode is Ground groundNode)
            {
                playerHealth.reduceHealth(2);
            }

            if (playerHealth.HasZeroHealth)
            {
                playerState = PlayerState.Destroyed;
            }
        }

        /// <summary>
        /// Update the Enemy Target Position.
        /// </summary>
        private void UpdateEnemyTargetPosition()
        {
            var distanceFromPlayer = Velocity.Length() / SpeedAtMaxEnemyTargetDistance * MaxEnemyTargetDistance;

            distanceFromPlayer = Math.Clamp(distanceFromPlayer, 0, MaxEnemyTargetDistance);
            var playerXDirectionSign = MathF.Sign(Velocity.X);
            enemyTarget.LocalPosition = new Vector2(distanceFromPlayer * playerXDirectionSign, 0);
        }
    }
}
