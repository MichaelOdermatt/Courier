using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Extensions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.TownCode;
using Courier.Game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        // TODO instead of using a callback for resetCurrentScene maybe submit an event to a scene manager?
        private readonly Action resetCurrentScene;
        private readonly PlayerInput playerInput = new PlayerInput();
        private readonly PlayerFuel playerFuel;
        private readonly PlayerMovement playerMovement;
        private readonly PlayerHealth playerHealth = new PlayerHealth();
        private readonly PlayerParcelDelivery playerParcelDelivery;

        private PlayerState playerState = PlayerState.Alive;

        private const float MaxEnemyTargetDistance = 500f;
        private const float SpeedAtMaxEnemyTargetDistance = 12f;

        /// <summary>
        /// The GlobalPosition of the EnemyTarget object.
        /// </summary>
        public Vector2 EnemyTargetGlobalPosition { get => enemyTarget.GlobalPosition; }

        public Player(
            Node parent, 
            Camera2D camera, 
            Action resetCurrentScene
        ) : base(parent)
        {
            var hub = Hub.Default;

            playerFuel = new PlayerFuel(hub);
            playerMovement = new PlayerMovement(playerInput, playerFuel);
            playerParcelDelivery = new PlayerParcelDelivery(this, playerFuel, hub);

            sprite = new Sprite(this, "Player");
            Children.Add(sprite);

            enemyTarget = new Node(this);
            Children.Add(enemyTarget);

            CollisionShape = new CollisionSphere(this, 6f);

            this.resetCurrentScene = resetCurrentScene;
            this.camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            playerInput.UpdateKeyboardState();

            // We put this above the check for playerState since we still want this input to work if the player is destroyed.
            if (playerInput.HasPlayerPressedQuickRestartKey())
            {
                resetCurrentScene();
            }

            if (playerState == PlayerState.Destroyed)
            {
                return; 
            }

            if (playerInput.HasPlayerPressedDeliverParcelKey())
            {
                playerParcelDelivery.DropParcel();
            }

            Velocity = playerMovement.CalcNewVelocity(gameTime, Velocity);
            ApplyVelocity();

            // Update the sprites rotation to match the angle of attack.
            var angleOfAttack = playerMovement.CalcAngleOfAttack(Velocity);
            sprite.Rotation = angleOfAttack;

            UpdateEnemyTargetPosition();
            UpdateCameraPosition();
        }

        public override void OnCollision(ICollisionNode collisionNode)
        {
            if (collisionNode is BulletBase bulletNode)
            {
                playerHealth.reduceHealth(1);
            } else if (collisionNode is Ground groundNode)
            {
                playerHealth.reduceHealth(2);
            }

            if (playerHealth.HasZeroHealth)
            {
                playerState = PlayerState.Destroyed;
            }
        }

        private void UpdateCameraPosition()
        {
            var newCameraPos = GlobalPosition;
            if (newCameraPos.Y <= -200)
            {
                newCameraPos.Y = -200;
            }
            // Update the camera position to always follow the Player.
            camera.SetPosition(newCameraPos);
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
