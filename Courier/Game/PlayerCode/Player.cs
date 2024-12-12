using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class Player : Node
    {
        private readonly Sprite sprite;
        private readonly CollisionNode collisionNode;
        private const CollisionNodeType CollisionType = CollisionNodeType.Player;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.SmallBullet, CollisionNodeType.LargeBullet, CollisionNodeType.Ground, CollisionNodeType.RefuelPoint };

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
        private readonly PlayerHealth playerHealth;
        private readonly PlayerParcelDelivery playerParcelDelivery;
        private readonly PlayerWantedLevel playerWantedLevel;

        private PlayerState playerState = PlayerState.Alive;

        private const float MaxEnemyTargetDistance = 500f;
        private const float SpeedAtMaxEnemyTargetDistance = 12f;
        private const float MaxCameraYValue = 200f;

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

            playerHealth = new PlayerHealth(hub);
            playerFuel = new PlayerFuel(hub);
            playerMovement = new PlayerMovement(playerInput, playerFuel);
            playerParcelDelivery = new PlayerParcelDelivery(this, hub);
            playerWantedLevel = new PlayerWantedLevel(hub);

            sprite = new Sprite(this, "Player");
            Children.Add(sprite);

            enemyTarget = new Node(this);
            Children.Add(enemyTarget);

            var collisionShape = new CollisionSphere(this, 6f);
            collisionNode = new CollisionNode(this, collisionShape, CollisionType, collisionTypeMask);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);

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

            playerParcelDelivery.UpdateParcelDropCooldown(gameTime);
            if (playerInput.IsPlayerPressingDeliverParcelKey())
            {
                playerParcelDelivery.DropParcel();
            }

            playerMovement.UpdateMovement(gameTime);
            // Apply the velocity to the players position.
            LocalPosition += playerMovement.Velocity;

            // Update the sprites rotation to match the angle of attack.
            sprite.Rotation = playerMovement.AngleOfAttack;

            UpdateEnemyTargetPosition();
            UpdateCameraPosition();
        }

        public void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            switch (eventArgs.collisionNode.CollisionNodeType)
            {
                case CollisionNodeType.RefuelPoint:
                    playerFuel.SetFuelToMax();
                    break;
                case CollisionNodeType.SmallBullet:
                    playerHealth.reduceHealth(1);
                    break;
                case CollisionNodeType.LargeBullet:
                    playerHealth.reduceHealth(2);
                    break;
                case CollisionNodeType.Ground:
                    playerHealth.reduceHealth(5);
                    break;
            }

            if (playerHealth.HasZeroHealth)
            {
                playerState = PlayerState.Destroyed;
            }
        }

        private void UpdateCameraPosition()
        {
            var newCameraPos = GlobalPosition;
            if (newCameraPos.Y <= MaxCameraYValue)
            {
                newCameraPos.Y = MaxCameraYValue;
            }
            // Update the camera position to always follow the Player.
            camera.SetPosition(newCameraPos);
        }

        /// <summary>
        /// Update the Enemy Target Position.
        /// </summary>
        private void UpdateEnemyTargetPosition()
        {
            var distanceFromPlayer = playerMovement.Velocity.Length() / SpeedAtMaxEnemyTargetDistance * MaxEnemyTargetDistance;

            distanceFromPlayer = Math.Clamp(distanceFromPlayer, 0, MaxEnemyTargetDistance);
            var playerXDirectionSign = MathF.Sign(playerMovement.Velocity.X);
            enemyTarget.LocalPosition = new Vector2(distanceFromPlayer * playerXDirectionSign, 0);
        }
    }
}
